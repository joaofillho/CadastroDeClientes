function carregarGrid() {
    $.ajaxSetup({ cache: false });

    //Tradução dos labels e mensagens do Jquery Bootgrid
    var traducao = {
        all: "Todos",
        infos: "Exibindo {{ctx.start}} até {{ctx.end}} de {{ctx.total}} registros",
        loading: "Carregando",
        noResults: "Sem dados",
        refresh: "Atualizar",
        search: "Pesquisar"
    };

    var ConfiguraGrid = {
        ajax: true,
        labels: traducao,
        url: urlListar,
        searchSettings: {
            delay: 50,
            characters: 3
        },
        converters: {
            datetime: {
                from: function (value) { return moment(value); },
                to: function (value) { return moment(value).format('L'); }
            }
        },
        formatters: {
            "acoes": function (column, row) {
                return '<a class="btn btn-default" data-toggle="tooltip" data-placement="left" title="Ver detalhes" href="javascript:void(0)" data-acao="Details" data-row-id=' + row.ClienteId + '><span class="glyphicon glyphicon-list" aria-hidden="true"></span></a>' +
                        '<a class="btn btn-default" data-toggle="tooltip" data-placement="top" title="Editar" href="javascript:void(0)" data-acao="Edit" data-row-id=' + row.ClienteId + '><span class="glyphicon glyphicon-edit" aria-hidden="true"></span></a>' +
                        '<a class="btn btn-default" data-toggle="tooltip" data-placement="right" title="Excluir" href="javascript:void(0)" data-acao="Delete" data-row-id=' + row.ClienteId + '><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a>';
            }
        }


    }

    //Aplica o Jquery Bootgrid à tabela de clientes carregado as configurações setadas acima
    var grid = $("#tbTabela").bootgrid(ConfiguraGrid);

    grid.on("loaded.rs.jquery.bootgrid", function () {
        grid.find("a.btn").each(function (index, elemento) {
            var btnAcao = $(this);
            var acao = btnAcao.data("acao");
            var idEntidade = btnAcao.data("row-id");

            btnAcao.on("click", function () {
                AbrirModal(acao, idEntidade);
            });
        });

        $('[data-toggle="tooltip"]').tooltip();

    });

    
    //Acionado pelo botão da Action Create exibe janela modal para cadastro de um novo cliente
    $("a[data-action='Create']").on("click", function () {
        AbrirModal($(this).data("action"));
    });


    //Exibe mudal para as Actions Create, Edit e Delete
    function AbrirModal(acao, parametro) {
        var url = "/{ctrl}/{acao}/{parametro}";
        url = url.replace("{ctrl}", controller);
        url = url.replace("{acao}", acao);
        
        if (parametro != null) {
            url = url.replace("{parametro}", parametro);
        } else {
            url = url.replace("/{parametro}", "");
        }

        $("#modal_conteudo").load(url, function () {
            $("#modal_janela").modal("show");

        });
    }

}