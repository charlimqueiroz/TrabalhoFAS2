//$.noConflict();
//$.ajax({ cache: false });

(function ($) {
    $.fn.serializeObject = function () {
        "use strict";

        var result = {};
        var extend = function (i, element) {
            var node = result[element.name];

            if ('undefined' !== typeof node && node !== null) {
                if ($.isArray(node)) {
                    node.push(element.value);
                } else {
                    result[element.name] = [node, element.value];
                }
            } else {
                result[element.name] = element.value;
            }
        };

        $.each(this.serializeArray(), extend);
        return result;
    };

})($)

/////////////////////// Funções da aplicação

//$(document).ready(function () {

$('.novo').on('click', function (e) {
    e.preventDefault();
    window.location = $(this).attr('url-redirect');
});


// função para substituir botão de submit
$('.list-editar').click(function (e) {
    e.preventDefault();
    var id = $('input:radio[name=gridRadio]:checked').attr('data-value');

    if (id == undefined || id == '' || id == '0') {
        alerta('Nenhum registro foi selecionado');
    }
    else {
        window.location = $(this).attr('url-redirect') + '/' + id;
    }
});
$('.list-excluir').click(function (e) {
    e.preventDefault();
    var id = $('input:radio[name=gridRadio]:checked').attr('data-value');

    if (id == undefined || id == '' || id == '0') {
        alert('Nenhum registro foi selecionado');
    }
    else {

        var listAction = $('.table').attr('list-action');
        var deleteAction = $('.table').attr('delete-action');

        if (confirm('Deseja realmente excluir o registro?')) {
            $.ajax({
                url: deleteAction,
                data: { id: id },
                dataType: 'json',
                success: function (dados) {
                    $.ajax({
                        url: listAction,
                        cache: false,
                        dataType: 'html',
                        success: function (dados) {
                            $('.grid-view').empty().html(dados);
                        },
                        error: function (e) {
                            alert('Ocorreu erro ao excluir.');
                        }
                    })
                    alert('Registro excluído com sucesso.');
                },
                error: function (e) {
                    alert('Ocorreu erro ao excluir.');
                },
            });
        }
    }
});
$('.dd-list > li > a').each(function (index, item) {
    $(item).click(function (e) {
        e.preventDefault();
        var url = $(this).attr('url-modal');
        var id = $('input:radio[name=gridRadio]:checked').attr('data-value');
        if (url.indexOf('Editar') >= 0) {
            if (id == undefined || id == '' || id == '0') {
                alert('Nenhum registro foi selecionado');
                return;
            }
        }
        $.ajax({
            type: "GET",
            cache: false,
            url: url,
            data: { id: id }
        }).done(function (html) {
            $('.window-modal').empty().html(html);
        });

        $('.window-modal').dialog({
            resizable: false,
            modal: true,
            width: 800,
            position: { my: "center", at: "center", of: 'body' },
            create: function (event, ui) {
                $('.ui-dialog-titlebar').hide();
            }
        });
    });
});

var Submit = function () {
    var id = $('.id-registro').val();
    if (id != '' && id != undefined)
        id = decrypt(id);
    if (id == 0 || id == '' || id == undefined) {
        urlPost = $('.salvar-modal').attr('url-post-create');
    }
    else {
        urlPost = $('.salvar-modal').attr('url-post-edit');
    }
    var form = $('.cadForm').serializeObject();
    $.post(urlPost, form, function (dados) {
        if (dados.Sucesso) {
            Fechar();
            $.ajax({
                url: location.href,
                cache: false,
                dataType: 'html',
                success: function (grid) {
                    $('.grid-view').empty().html(grid);
                },
                error: function (e) {
                    alert('Ocorreu um erro ao atualizar lista de registros.');
                }
            })
            alert('Registro salvo com sucesso.');
        }
        else {
            Fechar();
            alert(dados.Mensagem);
        }
    }).error(function (e) {
        alert(e.Mensagem);
    });
}


$('.salvar-modal').click(function (e) {
    e.preventDefault();
    Submit();
});


var Fechar = function () {
    $('.window-modal').dialog('close');
    $('.window-modal').empty();
}

$('.botao-cancelar').on('click', function (e) {
    e.preventDefault();
    Fechar();
});
var sucesso = function (msg) {
    $('.sucesso-mensagem').html(msg);
    $('.msgsuccess').slideDown();
    setTimeout(function () {
        $('.msgsuccess').slideUp();
    }, 3000);
}
var erro = function (msg) {
    $('.erro-mensagem').html(msg);
    $('.msgerror').slideDown();
    setTimeout(function () {
        $('.msgerror').slideUp();
    }, 3000);
}
var alerta = function (msg) {
    $('.alerta-mensagem').html(msg);
    $('.msgalert').slideDown();
    setTimeout(function () {
        $('.msgalert').slideUp();
    }, 3000);
}
var info = function (msg) {
    $('.info-mensagem').html(msg);
    $('.msginfo').slideDown();
    setTimeout(function () {
        $('.msginfo').slideUp();
    }, 3000);
}

var alerta = function (msg, title) {
    $('.msgDialog').html(msg);
    $('.dialog').attr('title', title);
    $('.dialog').dialog({
        resizable: true,
        modal: true,
        buttons: {
            "OK": $('.dialog').dialog('close')
        }
    });
}
var alertConfirm = function (msg, title, confirmFunction) {
    $('.msgDialog').html(msg);
    $('.dialog').attr('title', title);
    $('.dialog').dialog({
        resizable: false,
        modal: true,
        buttons: {
            "OK": function () { $('.dialog').dialog('close'); confirmFunction(); },
            "CANCELAR": function () { $('.dialog').dialog('close'); }
        }
    });
}

var encrypt = function (id) {
    var result;
    $.ajax({
        type: "GET",
        cache: false,
        async: false,
        url: '/Base/Encrypt',
        dataType: 'json',
        data: { id: id }
    }).done(function (obj) {
        result = obj.id;
    });
    return result;
}
var decrypt = function (id) {
    var result;
    $.ajax({
        type: "GET",
        cache: false,
        async: false,
        url: '/Base/Decripty',
        dataType: 'json',
        data: { id: id }
    }).done(function (obj) {
        result = obj.id;
    });
    return result;
}

$(".buttons li > a").click(function () {
    var parent = $(this).parent();
    if (parent.find(".dd-list").length > 0) {
        var dropdown = parent.find(".dd-list");
        if (dropdown.is(":visible")) {
            dropdown.hide();
            parent.removeClass('active');
        } else {
            dropdown.show();
            parent.addClass('active');
        }
        return false;
    }
});

//});