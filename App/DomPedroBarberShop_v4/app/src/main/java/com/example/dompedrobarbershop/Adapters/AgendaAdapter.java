package com.example.dompedrobarbershop.Adapters;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.LayoutRes;
import androidx.annotation.NonNull;

import com.example.dompedrobarbershop.FinalizarCliente;
import com.example.dompedrobarbershop.R;
import com.example.dompedrobarbershop.model.Agenda;
import java.util.List;

public class AgendaAdapter extends ArrayAdapter<Agenda> {
    private Context context;
     List<Agenda> agendas;
     String id;

    public AgendaAdapter(@NonNull Context context, @LayoutRes int resource, List<Agenda> objetcts, String id){
        super(context, resource, objetcts);
        this.context=context;
        this.agendas=objetcts;
        this.id=id;
    }

    @Override
    public View getView(final int pos, View convertView, ViewGroup parent){
        final LayoutInflater inflater=(LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        View rowView = inflater.inflate(R.layout.list_agenda, parent,false);
        TextView txtCliente=(TextView) rowView.findViewById(R.id.txtCliente);
        TextView txtServico=(TextView) rowView.findViewById(R.id.txtServico);

        txtCliente.setText(String.format("", agendas.get(pos).getNome_cliente()));
        txtServico.setText(String.format("", agendas.get(pos).getServico()));
        rowView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick (View view) {
                Intent intent=new Intent(context, FinalizarCliente.class);
                intent.putExtra("cliente",agendas.get(pos).getNome_cliente());
                intent.putExtra("servico",agendas.get(pos).getServico());
                intent.putExtra("data",agendas.get(pos).getData());
                intent.putExtra("hora",agendas.get(pos).getHora());
                context.startActivity(intent);
            }
        });
        return rowView;
    }
}


