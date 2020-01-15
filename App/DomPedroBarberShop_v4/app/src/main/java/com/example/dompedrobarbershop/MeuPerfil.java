package com.example.dompedrobarbershop;

import android.content.Intent;
import android.os.Bundle;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

public class MeuPerfil extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_meu_perfil);

        TextView nome = findViewById(R.id.lblNome1);
        TextView cpf = findViewById(R.id.lblCPF1);
        TextView email = findViewById(R.id.lblAcesso);



       Bundle params = getIntent().getExtras();

        String nome1 = params.getString("nome");
        String email1 = params.getString("email");
        String cpf1 = params.getString("cpf");
        String id=params.getString("cod_funcionario");

//        String cod_funcionario = params.getString("cod_funcionario");



        nome.setText(nome1);
        cpf.setText(cpf1);
        email.setText(email1);

//        Bundle ida = new Bundle();
//        //login start main activity
//
//        params.putString("cod_funcionario",cod_funcionario);
//
//        Intent intent = new Intent(MeuPerfil.this, MinhaAgenda.class);
//
//        intent.putExtras(ida);
//        startActivity(intent);

    }


}