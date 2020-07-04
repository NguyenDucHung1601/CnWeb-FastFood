var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        
        $('#AddToCard').off('click').on('click', function () {
            var list = [];          
            list.push({
                Products: {
                    id_product: $('#quantity').data('id')
                },             
                Amount: $('#quantity').val()                
            });            
            

            $.ajax({
                url: '/ShopCart/AddToDetail',
                data: { product: JSON.stringify(list) },
                dataType: 'json',
                type: "POST",
                success: function (res) {
                    if (res.status == true) {
                        $('#txtCountCart')..empty();
                        $('#txtCountCart').append(res.count);
                        $('#txtTotal').empty();
                        $('#txtTotal').append(res.total);
                        alert("thêm thành công");
                       // window.location.href = "/ShopCart/Index?discountCode=" + dcCode;
                    }
                }
            });

        });

    }
}
cart.init();

$('#quantity').change(function () {
    var amount = $('#quantity').val();
    var price = $('#quantity').data('price');
    var money = amount * price;
    $('#txttotal').empty();
    $('#txttotal').append("$" + money + "VNĐ");
});