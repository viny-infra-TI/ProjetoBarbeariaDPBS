package com.example.dompedrobarbershop.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class Agenda {
    @SerializedName("COD_FUNCIONARIO")
    @Expose
    private String cod_funcionario;

    @SerializedName("NOME_CLIENTE")
    @Expose
    private String nome_cliente;

    @SerializedName("SERVICO")
    @Expose
    private String servico;

    @SerializedName("DATA")
    @Expose
    private String data;

    @SerializedName("HORA")
    @Expose
    private String hora;

    public String getCod_funcionario () {
        return cod_funcionario;
    }

    public void setCod_funcionario (String cod_funcionario) {
        this.cod_funcionario = cod_funcionario;
    }

    public String getNome_cliente () {
        return nome_cliente;
    }

    public void setNome_cliente (String nome_cliente) {
        this.nome_cliente = nome_cliente;
    }

    public String getServico () {
        return servico;
    }

    public void setServico (String servico) {
        this.servico = servico;
    }

    public String getData () {
        return data;
    }

    public void setData (String data) {
        this.data = data;
    }

    public String getHora () {
        return hora;
    }

    public void setHora (String hora) {
        this.hora = hora;
    }

    public Agenda (String cod_funcionario, String nome_cliente, String servico, String data, String hora) {
        this.cod_funcionario = cod_funcionario;
        this.nome_cliente = nome_cliente;
        this.servico = servico;
        this.data = data;
        this.hora = hora;
    }
}
