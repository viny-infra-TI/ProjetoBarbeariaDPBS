package com.example.dompedrobarbershop;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

public class TelaMenu extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tela_menu);

        Button perfil = findViewById(R.id.btnPerfil);
        Button agenda = findViewById(R.id.btnAgenda);


        perfil.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
             /*   Intent intent = new Intent(TelaMenu.this, MeuPerfil.class);
                startActivity(intent);*/

                Bundle parametros = getIntent().getExtras();

                String nome1 = parametros.getString("nome");
                String email = parametros.getString("email");
                String cpf = parametros.getString("cpf");
//                String cod_funcionario = parametros.getString("cod_funcionario");




                Bundle params = new Bundle();
                //login start main activity

                params.putString("email", email);
                params.putString("nome", nome1);
                params.putString("cpf", cpf);
//                params.putString("cod_funcionario",cod_funcionario);

                Intent intent = new Intent(TelaMenu.this, MeuPerfil.class);

                intent.putExtras(params);
                startActivity(intent);



            }
        });

        agenda.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {


                Bundle ida = getIntent().getExtras();
                String cod_funcionario = ida.getString("cod_funcionario");
                 String email = ida.getString("email");
                 String senha = ida.getString("senha");


                Bundle params = new Bundle();
                //login start main activity

                params.putString("cod_funcionario",cod_funcionario);
                params.putString("email", email);
                params.putString("senha", senha);

                Intent intent = new Intent(TelaMenu.this, AgendaFinaliza.class);

                intent.putExtras(params);
                startActivity(intent);
            }
        });

    }

    public boolean onKeyDown(int keyCode, KeyEvent event) {
        //Handle the back button
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            checkExit();
            return true;
        } else {
            return super.onKeyDown(keyCode, event);
        }
    }

    private void checkExit() {

        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setIcon(R.drawable.teste);
        builder.setTitle("Saindo...");
        builder.setMessage("Deseja realmente sair?");
        builder.setCancelable(false);
        builder.setPositiveButton("Sim", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int id) {
                finish();

            }
        })
                .setNegativeButton("Não", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });
        AlertDialog alert = builder.create();
        alert.show();



    }
}


