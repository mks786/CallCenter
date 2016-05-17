(function ($) {
    $.fn.extend({
        jqsimplemenu2: function () {
            return this.each(function () {
                //add class .drop-down to all of the menus having drop-down items
                var menu2 = $(this);
                var timeoutInterval;
                if (!menu2.hasClass('menu2')) menu2.addClass('menu2');
                $("> li", menu2).each(function () {
                    if ($(this).find("ul:first").length > 0)
                        $(this).addClass('pull-down');
                });

                $("> li ul li ul", menu2).each(function () {
                    $(this).parent().addClass('right-menu2');
                });
                $("li", menu2).mouseenter(function () {
                    var isTopLevel = false;
                    //if its top level then add animation 
                    isTopLevel = $(this).parent().attr('class') === 'menu2';
                    if (isTopLevel) {
                        clearTimeout(timeoutInterval);
                        var w = $(this).outerWidth();
                        // if ($(this).hasClass('pull-down')) w += 10;
                        var h = $(this).outerHeight();
                        var box = $('<div/>').addClass('box');
                        $('> li', menu2).removeClass('selected');
                        $('>li div.box', menu2).remove();
                        $('>li ul', menu2).css('display', 'none').slideUp(0);
                        $(this).prepend(box);
                        $(this).addClass('selected');
                        box.stop(true, false).animate({ width: w, height: h }, 0, function () {
                            if ($(this).parent().find('ul:first').length == 0) {
                                timeoutInterval = setTimeout(function () {
                                    box.stop(true, false).animate({ height: '+=5' }, 0, function () {
                                        box.parent().find('ul:first').css('display', 'block').css('top', box.height()).stop(true, false).slideDown(300);
                                    });
                                }, 10);
                            }
                            else {

                                timeoutInterval = setTimeout(function () {
                                    box.stop(true, false).animate({ height: '+=0' }, 0, function () {
                                        box.parent().find('ul:first').css('display', 'block').css('top', box.height()).stop(true, false).slideDown(300);
                                    });
                                }, 10);
                            }
                        });
                    }
                    else {
                        $(this).find('ul:first').css('display', 'block');
                    }

                });
                $("li", menu2).mouseleave(function () {
                    isTopLevel = $(this).parent().attr('class') === 'menu2';
                    if (isTopLevel) {
                        $(this).parent().find('div.box').remove();
                    }
                    $(this).find('ul').slideUp(300, function () {

                        $(this).css('display', 'none');
                    });
                });
                $("li", menu2).click(function () {
                    var isTopLevel = false;
                    //if its top level then add animation 
                    isTopLevel = $(this).parent().attr('class') === 'menu2';
                    if (isTopLevel) {
                        clearTimeout(timeoutInterval);
                        var w = $(this).outerWidth();
                        // if ($(this).hasClass('pull-down')) w += 10;
                        var h = $(this).outerHeight();
                        var box = $('<div/>').addClass('box');
                        $('> li', menu2).removeClass('selected');
                        $('>li div.box', menu2).remove();
                        $('>li ul', menu2).css('display', 'none').slideUp(100);
                        $(this).prepend(box);
                        $(this).addClass('selected');
                        box.stop(true, false).animate({ width: w, height: h }, 100, function () {
                            if ($(this).parent().find('ul:first').length == 0) {
                                timeoutInterval = setTimeout(function () {
                                    box.stop(true, false).animate({ height: '+=5' }, 300, function () {
                                        box.parent().find('ul:first').css('display', 'block').css('top', box.height()).stop(true, false).slideDown(300);
                                    });
                                }, 10);
                            }
                            else {

                                timeoutInterval = setTimeout(function () {
                                    box.stop(true, false).animate({ height: '+=0' }, 0, function () {
                                        box.parent().find('ul:first').css('display', 'block').css('top', box.height()).stop(true, false).slideDown(300);
                                    });
                                }, 10);
                            }
                        });
                    }
                    else {
                        $(this).find('ul:first').css('display', 'block');
                    }

                })
                $('> li > ul li a', menu2).hover(function () {
                    $(this).parent().addClass('menu-item-selected');
                }, function () {

                    $(this).parent().removeClass('menu-item-selected');
                });

            });
        }
    });
})(jQuery);

