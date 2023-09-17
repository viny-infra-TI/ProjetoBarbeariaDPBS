package com.example.dompedrobarbershop;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

//import com.wdullaer.materialdatetimepicker.date.TextViewWithCircularIndicator;

public class FinalizarCliente extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_finalizar_cliente);

        TextView nome = findViewById(R.id.nomeCliente);
        TextView dta = findViewById(R.id.data1);
        TextView hour = findViewById(R.id.hora);
        TextView serv = findViewById(R.id.serviçoAgendado);



        Bundle extras = getIntent().getExtras();
        String cliente = extras.getString("cliente");
        String data = extras.getString("data");
        String hora = extras.getString("hora");
        String servico=extras.getString("servico");




            nome.setText(cliente);
            dta.setText(data);
            hour.setText(hora);
            serv.setText(servico);



        Button btnTerminar = findViewById(R.id.btnFinalizaCliente);

        btnTerminar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });




    }
}
