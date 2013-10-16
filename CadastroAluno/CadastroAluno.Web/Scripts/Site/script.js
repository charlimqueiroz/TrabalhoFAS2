/* --------------------------------- */
/* COLOQUE AS FUNÇÕES DENTRO DO noConflict para não ter problemas de interface. =)*/
/*---------------------------------*/

var $ = jQuery.noConflict();

$(document).ready(function($) {
	/* ---------------------------------------------------------------------- */
	/*	Sliders - [Flexslider]
	/* ---------------------------------------------------------------------- */
  	try {
		$('.flexslider').flexslider({
			animation: "fade",
		});
	} catch(err) {

	}

	/*-------------------------------------------------*/
	/* =  Input & Textarea Placeholder
	/*-------------------------------------------------*/
	$('input[type="text"], textarea').focus(function(){
		$(this).removeClass('error');
		if ($(this).val().toLowerCase() == $(this).attr('data-value').toLowerCase())
			$(this).val('');
	}).blur( function(){ 
		if ($(this).val() == '')
			$(this).val($(this).attr('data-value'));
	});

	/*-------------------------------------------------*/
	/* =  Dropdown Menu - Superfish
	/*-------------------------------------------------*/
	try {
		$('ul.sf-menu').superfish({
			delay: 400,
			autoArrows: false,
			speed: 'fast',
			animation: {opacity:'show', height:'show'}
		});
	} catch (err){

	}

	/*-------------------------------------------------*/
	/* =  Mobile Menu
	/*-------------------------------------------------*/
	// Create the dropdown base
    $("<select />").appendTo(".navigation");
    
    // Create default option "Go to..."
    $("<option />", {
    	"selected": "selected",
    	"value"   : "",
    	"text"    : "Ir para..."
    }).appendTo(".navigation select");
    
    // Populate dropdown with menu items
    $(".sf-menu a").each(function() {
    	var el = $(this);
    	if(el.next().is('ul.sub-menu')){
    		$("<optgroup />", {
	    	    "label"    : el.text()
	    	}).appendTo(".navigation select");
    	} else {
    		$("<option />", {
	    	    "value"   : el.attr("href"),
	    	    "text"    : el.text()
	    	}).appendTo(".navigation select");
    	}
    });

    $(".navigation select").change(function() {
      window.location = $(this).find("option:selected").val();
    });

	/*-------------------------------------------------*/
	/* =  Fancybox Images
	/*-------------------------------------------------*/
	try {
		$(".gallery a").fancybox({
			nextEffect	: 'fade',
			prevEffect	: 'fade',
			openEffect	: 'fade',
	    	closeEffect	: 'fade',
	          helpers: {
	              title : {
	                  type : 'float'
	              }
	          }
		});
	} catch(err) {

	}

	/*-------------------------------------------------*/
	/* =  Fancybox Videos
	/*-------------------------------------------------*/
	try {
		$('.video .post-image a').fancybox({
			maxWidth	: 800,
			maxHeight	: 600,
			fitToView	: false,
			width		: '75%',
			height		: '75%',
			type 		: 'iframe',
			autoSize	: false,
			closeClick	: false,
			openEffect	: 'fade',
			closeEffect	: 'fade'
		});
	} catch(err) {

	}

	/*-------------------------------------------------*/
	/* =  Scroll to TOP
	/*-------------------------------------------------*/
	$('a[href="#top"]').click(function(){
        $('html, body').animate({scrollTop: 0}, 'slow');
        return false;
    });

    /*-------------------------------------------------*/
	/* =  Tabs Widget - { Mais Pop, Recentes e Comentarios}
	/*-------------------------------------------------*/
	$('.tab-links li a').live('click', function(e){
		e.preventDefault();
		if (!$(this).parent('li').hasClass('active')){
			var link = $(this).attr('href');

			$(this).parents('ul').children('li').removeClass('active');
			$(this).parent().addClass('active');

			$('.tabs-widget > div').hide();

			$(link).fadeIn();
		}
	});
});

/*-------------------------------------------------*/
/* =  Masonry Effect
/*-------------------------------------------------*/
$(window).load(function(){
	try {
		$('#sidebar').masonry({
			singleMode: true,
			itemSelector: '.widget',
			columnWidth: 295,
			gutterWidth: 20
		});
	} catch(err) {

	}
});