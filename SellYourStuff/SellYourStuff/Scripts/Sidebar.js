function openNav() {
    document.getElementById("mySidenav").style.width = "100%";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

jQuery(document).ready(function () {
    var overlay = $('.menu-bg-overlay');
    var close = $('.menu-close');
    var trigger = $('.menu-trigger');
    var modal = $('.menu-overlay');

    trigger.click(function () {
        var removeModal = function () {
            modal.removeClass('menu-overlay-show');
        }

        modal.addClass('menu-overlay-show');
        overlay.off('click', removeModal);
        overlay.on('click', removeModal);
    });

    close.click(function (e) {
        e.stopPropagation();
        modal.removeClass('menu-overlay-show');
    });
});



