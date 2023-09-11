$(document).ready(function () {

    /* DataTables start here. */

    const dataTable = $('#productsTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {productsTable
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Product/GetAllProducts/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#productsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const productListDto = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(productListDto);
                            if (productListDto.ResultStatus === 0) {
                                $.each(productListDto.Products.$values,
                                    function (index, product) {
                                        const newTableRow = dataTable.row.add([
                                            product.Id,
                                            product.Code,
                                            product.Name,
                                            product.Price,
                                            `<img src="/image/${product.Picture}" alt="${product.Name}" class="my-image-table" />`,
                                            (product.IsDeleted == 0 ) ? 'Hayır' : 'Evet',
                                            product.CreatedDate,
                                            product.ModifiedDate,
                                            `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${product.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${product.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `${product.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#productsTable').fadeIn(1400);
                            } else {
                                toastr.error(`${productListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#productsTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

    /* DataTables end here */

    /* Ajax GET / Getting the _ProductAddPartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Product/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Ajax GET / Getting the _ProductAddPartial as Modal Form ends here. */

        /* Ajax POST / Posting the FormData as ProductAddDto starts from here. */

        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-product-add');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        console.log(data);
                        const productAddAjaxModel = jQuery.parseJSON(data);
                        console.log(productAddAjaxModel);
                        const newFormBody = $('.modal-body', productAddAjaxModel.ProductAddPartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');
                            const newTableRow = dataTable.row.add([
                                productAddAjaxModel.ProductDto.Product.Id,
                                productAddAjaxModel.ProductDto.Product.Code,
                                productAddAjaxModel.ProductDto.Product.Name,
                                productAddAjaxModel.ProductDto.Product.Price,
                                `<img src="/image/${productAddAjaxModel.ProductDto.Product.Picture}" alt="${productAddAjaxModel.ProductDto.Product.Name}" class="my-image-table" />`,
                                `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${productAddAjaxModel.ProductDto.Product.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${productAddAjaxModel.ProductDto.Product.Id}"><span class="fas fa-minus-circle"></span></button>
                            `
                            ]).node();
                            const jqueryTableRow = $(newTableRow);
                            jqueryTableRow.attr('name', `${productAddAjaxModel.ProductDto.Product.Id}`);
                            dataTable.row(newTableRow).draw();
                            toastr.success(`${productAddAjaxModel.ProductDto.Message}`, 'Başarılı İşlem!');
                        } else {
                            let summaryText = "";
                            $('#validation-summary > ul > li').each(function () {
                                let text = $(this).text();
                                summaryText = `*${text}\n`;
                            });
                            toastr.warning(summaryText);
                        }
                    },
                    error: function (err) {
                        toastr.error(`${err.responseText}`, 'Hata!');
                    }
                });
            });
    });

    /* Ajax POST / Posting the FormData as ProductAddDto ends here. */

    /* Ajax POST / Deleting a Product starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            //const ProductName = tableRow.find('td:eq(1)').text();

            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `Ürün silinicektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, silmek istiyorum.',
                cancelButtonText: 'Hayır, silmek istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { productId: id },
                        url: '/Admin/Product/Delete/',
                        success: function (data) {
                            const prodcutDto = jQuery.parseJSON(data);
                            if (prodcutDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `Ürün başarıyla silinmiştir.`,
                                    'success'
                                );

                                dataTable.row(tableRow).remove().draw();
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${prodcutDto.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!")
                        }
                    });
                }
            });
        });

    /* Ajax GET / Getting the _ProductUpdatePartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Product/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');

        //var id = 0;
        $(document).on('click',
            '.btn-update',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                console.log(id);
                $.get(url, { productId: id }).done(function (data) {
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');
                }).fail(function () {
                    toastr.error("Bir hata oluştu.");
                });
            });

        /* Ajax POST / Updating a Product starts from here */

        placeHolderDiv.on('click',
            '#btnUpdate',
            function (event) {
                event.preventDefault();

                const form = $('#form-product-update');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                console.log(dataToSend);
                //debugger;
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        const productUpdateAjaxModel = jQuery.parseJSON(data);
                        console.log(productUpdateAjaxModel);
                        let productId;
                        let tableRow;
                        if (productUpdateAjaxModel.ProductDto !== null) {
                            //console.log("ProductId:"+ productId);
                            productId = productUpdateAjaxModel.ProductDto.Product.Id;
                            tableRow = $(`[name="${productId}"]`);
                        }



                        const newFormBody = $('.modal-body', productUpdateAjaxModel.ProductUpdatePartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';

                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');
                            dataTable.row(tableRow).data([
                                productUpdateAjaxModel.ProductDto.Product.Id,
                                productUpdateAjaxModel.ProductDto.Product.Code,
                                productUpdateAjaxModel.ProductDto.Product.Name,
                                productUpdateAjaxModel.ProductDto.Product.Price,
                                `<img src="/image/${productUpdateAjaxModel.ProductDto.Product.Picture}" alt="${productUpdateAjaxModel.ProductDto.Product.Name}" class="my-image-table" />`,
                                (productUpdateAjaxModel.ProductDto.Product.IsDeleted ? "Evet" : "Hayır"),
                                productUpdateAjaxModel.ProductDto.Product.CreatedDate,
                                productUpdateAjaxModel.ProductDto.Product.ModifiedDate,
                                                               `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${productUpdateAjaxModel.ProductDto.Product.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${productUpdateAjaxModel.ProductDto.Product.Id}"><span class="fas fa-minus-circle"></span></button>
                            `
                            ]);
                            tableRow.attr("name", `${id}`);
                            dataTable.row(tableRow).invalidate();
                            toastr.success(`${productUpdateAjaxModel.ProductDto.Message}`, "Başarılı İşlem!");
                        } else {
                            let summaryText = "";
                            $('#validation-summary > ul > li').each(function () {
                                let text = $(this).text();
                                summaryText = `*${text}\n`;
                            });
                            toastr.warning(summaryText);
                        }
                    },
                    error: function (err) {
                        toastr.error(`${err.responseText}`, 'Hata!');
                    }
                });
            });

    });
});