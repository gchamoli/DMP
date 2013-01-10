window.onscroll = function () {
    //    if (window.XMLHttpRequest) {
    //        if (document.documentElement.scrollTop > 0 || self.pageYOffset > 0) {
    //            jQuery('#primary_left').css('position', 'fixed');
    //            jQuery('#primary_left').css('top', '0');
    //        } else if (document.documentElement.scrollTop < 0 || self.pageYOffset < 0) {
    //            jQuery('#primary_left').css('position', 'absolute');
    //            jQuery('#primary_left').css('top', '175px');
    //        }
    //    }
};

function initMenu() {
    //    jQuery('#menu ul ul').hide();
    //    jQuery('#menu ul li').click(function () {
    //        jQuery(this).parent().find("ul").slideUp('fast');
    //        jQuery(this).parent().find("li").removeClass("current");
    //        jQuery(this).find("ul").slideToggle('fast');
    //        jQuery(this).toggleClass("current");
    //    });
}


$(document).ready(function () {

    //    Cufon.replace('h1, h2, h5, .notification strong', { hover: 'true' }); // Cufon font replacement
    //    initMenu(); // Initialize the menu!

    //  
    //    $('#menu li:not(".current"), #menu ul ul li a').hover(function () {
    //        jQuery(this).find('span').animate({ marginLeft: '5px' }, 100);
    //    }, function () {
    //        jQuery(this).find('span').animate({ marginLeft: '0px' }, 100);
    //    }); // Menu simple animation

    //    $('.fade_hover').hover(
    //		function () {
    //		    jQuery(this).stop().animate({ opacity: 0.6 }, 200);
    //		},
    //		function () {
    //		    jQuery(this).stop().animate({ opacity: 1 }, 200);
    //		}
    //	);
});

function closePopUp() {
    $.fancybox.close();
}

function reLoad(url) {
    window.location.href = url;
}
    

