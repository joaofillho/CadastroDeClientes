
    function VerificaCPF(valorDigitado) {
        //Ex:417.216.042-51
        //Chama função para remover caracteres que não são números
        //Ex:Retorna 41721604251
        var cpfLimpo = valorDigitado.replace(/[^0-9]/g, '');

        if (cpfLimpo.length != 11) {
            return false;
        }
            

                
        //1º digito verificador a partir das 9 posicoes iniciais
        //Ex: 417216042
        var cpf_temp = cpfLimpo.substr(0, 9);

        //1º dígito verificador
        var cpf_temp = MontaCPF(cpf_temp, 10);
        
        //2º dígito verificador
        var cpf_temp = MontaCPF(cpf_temp, 11);

        //Descartando cpfs inválidos que passariam pela validação
        if (cpf_temp.length != 11 || cpf_temp == "00000000000" || cpf_temp == "11111111111" || cpf_temp == "22222222222" || cpf_temp == "33333333333" || cpf_temp == "44444444444" || cpf_temp == "55555555555" || cpf_temp == "66666666666" || cpf_temp == "77777777777" || cpf_temp == "88888888888" || cpf_temp == "99999999999")
            return false;

        // Retorna true para cpf válido
        if (cpfLimpo == cpf_temp) {
            $('#idcpf').val(cpf_temp);
            return true
        }else{
            return false;
        }
            
   
    }


    

    //Monta um CPF válido calculando os dígitos verificadores
    function MontaCPF(cpfParcial, posicoes){
            
        var soma =0;

        for (var i = 0; i < cpfParcial.length; i++) {
            soma = soma + (cpfParcial[i] * posicoes );
            posicoes--;
            
        }

        //Caso o valor do resto da divisão seja menor que 2, esse valor passa automaticamente a ser zero, 
        //caso contrário (como no nosso caso) é necessário subtrair o valor obtido de 11 para se obter o dígito verificador.         soma = soma % 11;
        
        if(soma < 2){
            soma=0;
        }else{
            soma = 11 - soma;
        }

        cpfParcial = cpfParcial.toString();
        //Concatena a primeira parte do cpf com o primeiro dígito verificador
        cpfParcial = cpfParcial + soma;

        

        return cpfParcial;

    }
