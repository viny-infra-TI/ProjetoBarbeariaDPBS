package com.example.dompedrobarbershop;

import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.widget.EditText;
import android.widget.ListView;

import androidx.appcompat.app.AppCompatActivity;

import com.example.dompedrobarbershop.Adapters.AgendaAdapter;
import com.example.dompedrobarbershop.Conexao.AgendaService;
import com.example.dompedrobarbershop.Conexao.ApiUtils;
import com.example.dompedrobarbershop.model.Agenda;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AgendaFinaliza extends AppCompatActivity {
    ListView listView;
    AgendaService agendaService;
    List<Agenda> list = new ArrayList<Agenda>();
    String id;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_agenda_finializa);
        inicializa();

    }

    public void inicializa(){
        listView=(ListView) findViewById(R.id.listView);
        agendaService= ApiUtils.getAgendaService();
        getAgendaList();
    }

    public void getAgendaList() {
            Bundle extras = getIntent().getExtras();
            id= extras.getString("cod_funcionario");

            Call<List<Agenda>> call=agendaService.getAgenda(Integer.parseInt(id));
            call.enqueue(new Callback<List<Agenda>>() {
                @Override
                public void onResponse (Call<List<Agenda>> call, Response<List<Agenda>> response) {
                    if(response.isSuccessful()){
                        list=response.body();
                        listView.setAdapter(new AgendaAdapter(AgendaFinaliza.this,R.layout.list_agenda, list,id));
                    }
                }

                @Override
                public void onFailure (Call<List<Agenda>> call, Throwable t) {
                    Log.e("ERRO: ", t.getMessage());
                }
            });

//        //Aqui é aonde adicionamos nosso filtro no edittext.
//        EditText editText = (EditText) findViewById(R.id.edtAgendamento);
//        editText.addTextChangedListener(new TextWatcher() {
//            @Override
//            public void onTextChanged(CharSequence s, int start, int before,
//                                      int count) {
//                //quando o texto é alterado chamamos o filtro.
//                listView.getTextFilter();
//            }
//
//            @Override
//            public void beforeTextChanged(CharSequence s, int start, int count,
//                                          int after) {
//            }
//
//            @Override
//            public void afterTextChanged(Editable s) {
//            }
//        });

    }
}

















































































//
//
//        final ListView listView = findViewById(R.id.listView);
//
//
//        final ArrayList<String> arrayList = new ArrayList<>();
//
//        arrayList.add("Leandro");
//        arrayList.add("Cristiano");
//        arrayList.add("Vinicius");
//        arrayList.add("Thomas");
//        arrayList.add("Leandro");
//        arrayList.add("Cristiano");
//        arrayList.add("Vinicius");
//        arrayList.add("Thomas");
//        arrayList.add("Leandro");
//        arrayList.add("Cristiano");
//        arrayList.add("Vinicius");
//        arrayList.add("Thomas");
//
//
//        final ArrayAdapter arrayAdapter =    new ArrayAdapter(this, android.R.layout.simple_selectable_list_item, arrayList);
//
//        listView.setAdapter(arrayAdapter);
//
//
//
//        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
//            @Override
//            public void onItemClick(AdapterView<?> adapterView, View view, int i, long id) {
//
//                Intent thomas= new Intent(AgendaFinaliza.this, FinalizarCliente.class);
//                thomas.setAction(Intent.ACTION_VIEW);
//                thomas.setData(Uri.parse("Leandro"));
//                startActivity(thomas);
//
//                Toast.makeText(AgendaFinaliza.this, "Cliente: "+ arrayList.get(i), Toast.LENGTH_SHORT).show();
//            }
//        });



