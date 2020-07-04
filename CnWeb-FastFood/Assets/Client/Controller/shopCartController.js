var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/Home/Index";
        });

        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtquantity');
            var cartList = [];
            var dcCode = $('#txtdiscountCode').val();
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Amount: $(item).val(),
                    Products: {
                        id_product: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/ShopCart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShopCart/Index?discountCode=" + dcCode;
                    }
                }
            });      

        });

        $('#btnDeleteAll').off('click').on('click', function () {
           
            $.ajax({
                url: '/ShopCart/DeleteAll',
              
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/ShopCart/Index/";
                    }
                }
            });

        });
    }
}
cart.init();