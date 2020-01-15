package com.example.dompedrobarbershop;

import android.content.Intent;
import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import androidx.recyclerview.widget.SortedList;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.SortedMap;
import java.util.TreeMap;





import com.example.dompedrobarbershop.Conexao.ApiUtils;
import com.example.dompedrobarbershop.Conexao.LoginService;
import com.example.dompedrobarbershop.model.Login;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;



public class MainActivity extends AppCompatActivity {


    EditText edEmail;
    EditText edSenha;
    Button btnLogar;
    LoginService loginService;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        edEmail = (EditText) findViewById(R.id.lblEmail);
        edSenha = (EditText) findViewById(R.id.senha);
        btnLogar = (Button) findViewById(R.id.btnEntrar);
        loginService = ApiUtils.getLoginService();

        btnLogar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email = edEmail .getText().toString();
                String senha = edSenha.getText().toString();
                //validate form
                if(validaCampo(email, senha)){
                    //do login
                    doLogin(email, senha);
                }

            }
        });

    }

    private boolean validaCampo(String email, String senha){
        if(email == null || email.trim().length() == 0){
            Toast.makeText(this, "Preencha o Email", Toast.LENGTH_SHORT).show();
            return false;
        }
        if(senha == null || senha.trim().length() == 0){
            Toast.makeText(this, "Preencha a Senha", Toast.LENGTH_SHORT).show();
            return false;
        }
        return true;
    }
    private void doLogin(final String email, final String senha){
        SortedMap<String, String> data = new TreeMap<String, String>();
        data.put("EMAIL", email);
        data.put("SENHA", senha);


        Call<List<Login>> call=loginService.getLogin(data);
        // Call<Login> call = loginService.login("login=" + username,"&senha=" + password);
        call.enqueue(new Callback<List<Login>>(){
            @Override
            public void onResponse(Call<List<Login>> call, Response<List<Login>> response) {
                if(response.isSuccessful()){
                    final List<Login> resultado= response.body();
                    if(!resultado.isEmpty()) {

                        Bundle parametros = new Bundle();
                        //login start main activity


                        parametros.putString("email", email);
                        parametros.putString("senha", senha);
                        parametros.putString("nome", resultado.get(0).getNome());
                        parametros.putString("cpf", resultado.get(0).getCpf());
                        parametros.putString("cod_funcionario",resultado.get(0).getId());




                        Intent intent = new Intent(MainActivity.this, TelaMenu.class);

                        intent.putExtras(parametros);
                        startActivity(intent);

                    }else {
                        Toast.makeText(MainActivity.this, "Email ou Senha incorreta!! ", Toast.LENGTH_SHORT).show();
                    }

                } else {
                    Toast.makeText(MainActivity.this, "Erro, Tente Novamente !!", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call call, Throwable t) {
                Toast.makeText(MainActivity.this, t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });



//        btnLogar.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                if (loginValido() == true) {
//                    Intent intent = new Intent(MainActivity.this, TelaMenu.class);
//                    startActivity(intent);
//
//                    Toast.makeText(getApplicationContext(), "Bem vindo", Toast.LENGTH_LONG).show();
//                }
//
//
//            }
//
//
//            private boolean loginValido() {
//
//
//                boolean isValid = true;
//
//                EditText campoComFoco = null;
//                String email1 = edLogin.getText().toString().trim();
//                String senha1 = edSenha.getText().toString().trim();
//
//
//                if (email1.isEmpty()) {
//                    edLogin.setError("Email Inválido");
//                    campoComFoco = edLogin;
//                    isValid = false;
//                } else if (!Patterns.EMAIL_ADDRESS.matcher(email1).matches()) {
//                    edLogin.setError("Email Inválido");
//                    campoComFoco = edLogin;
//                    isValid = false;
//                }
//
//                if (senha1.isEmpty()) {
//                    edSenha.setError("Senha Inválida");
//                    campoComFoco = edSenha;
//                    isValid = false;
//                } else isValid = true;
//                return isValid;
//            }
//
//
//        });
    }
}