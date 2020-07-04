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

        $('#checkout').on('click', function () {
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
            var bill = {
                Item: cartList,
                Id_customer :"",
                Subtotal : $('#txtSubTotal').text(),
                Total : $('#txtTotal').text()
            }

            $.ajax({
                url: '/ShopCheckout/AddBill',
                data: { billModel: JSON.stringify(bill) },
                dataType: 'json',
                type: "POST",                
            }).done(function (res) {
                window.location.href = res.newUrl;
            }).fail(function (xhr, a, error) {
                console.log(error);
            });

        });
    }
}
cart.init();