var BaseController = function () {
    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {
        $('body').on('click', '.cart-order', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            var quantity = $('.cart-plus-minus-box').val();
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'post',
                data: {
                    productId: id,
                    quantity: quantity == null ? 1 : quantity
                },
                success: function (response) {
                    $('.cart-plus-minus-box').val('1');
                    //tedu.notify(resources["AddCartOK"], 'success');
                    loadHeaderCart();
                }
            });
        });

        $('body').on('click', '.remove-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'post',
                data: {
                    productId: id
                },
                success: function (response) {
                   // tedu.notify(resources["RemoveCartOK"], 'success');
                    loadHeaderCart();
                }
            });
        });
    }

    function loadHeaderCart() {
        $("#headerCart").load("/AjaxContent/HeaderCart");
    }

}