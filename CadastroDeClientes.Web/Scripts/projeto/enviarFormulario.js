    $('#btnExcluir').on('click', excluir);

    $('input.btn').on('click', submeter);

    $('#idcpf').on('blur', validarFormulario);
    $('#idDataNascimento').on('blur', validarFormulario);

    //Submete os dados do formulário para validação
    function submeter(evento) {

        evento.preventDefault();
        var url = formulario.prop("action");

        if (validarFormulario()) {
            
            var metodo = formulario.prop("method");
            var dados = formulario.serialize();
                
            $.ajax({
                url: url,
                type: metodo,
                data: dados,
                success: trataRetorno
            });
        }

    }
    
    //Faz a validação do formulário não permitindo cadastro de menor de idade 
    //nem cpf inválido
    function validarFormulario() {
        
        var validado = false;

        if (formulario.valid == undefined) {
            validado = true;
        } else {
            validado = formulario.valid();
        }

               
        //Chama função para calcular a idade
        var idadeCalculada = calculaIdade();

        if (idadeCalculada < 18) {
            $("#alertaIdade").html("Não é permitido o cadastro de cliente menor de idade");
            validado = false;
        } else {
            $("#alertaIdade").html("");
        }


        //Chama função para calcular o cpf
        var cpf = $("#idcpf").val();

        if (!VerificaCPF(cpf)) {
            $("#alertaCpf").html("Cpf inválido.");
            validado = false;
        } else {
            $("#alertaCpf").html("");
        }
        
        return validado;
     }

    
    //Trata o retorno das validações dos campos do formulário
    function trataRetorno(formularioRetorno) {

        if (formularioRetorno.retorno) {
            toastr["success"](formularioRetorno.mensagem);
            $("#modal_janela").modal("hide");
            $("#tbTabela").bootgrid("reload");
        } else {
            toastr["error"](formularioRetorno.mensagem);
        }

    }
    
    //Exclusão de item selecionado
    function excluir(evento) {

        evento.preventDefault();
        var url = formulario.prop("action");
        var metodo = formulario.prop("method");
        var dados = formulario.serialize();

        $.ajax({
            url: url,
            type: metodo,
            data: dados,
            success: trataRetorno
        });
    }


    //Calcula a idade para impedir cadastro de cliente com idade inferior a 18 anos
    function calculaIdade() {

        var idade = 0;
        var txtnascimento = $("#idDataNascimento").val();
        var dmyNascimento = txtnascimento.toString().split("-");
        var anoNascimento = dmyNascimento[0];
        var mesNascimento = dmyNascimento[1];
        var diaNascimento = dmyNascimento[2];

        var date = new Date();
        var val = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate();
        var anoAtual = date.getFullYear();
        var mesAtual = (date.getMonth() + 1);
        var diaAtual = date.getDate();

        if ((mesAtual > mesNascimento) || (mesAtual == mesNascimento && diaAtual >= diaNascimento)) {
            idade = anoAtual - anoNascimento
        } else {
            idade = anoAtual - anoNascimento - 1;
        }

        return idade;

    }

