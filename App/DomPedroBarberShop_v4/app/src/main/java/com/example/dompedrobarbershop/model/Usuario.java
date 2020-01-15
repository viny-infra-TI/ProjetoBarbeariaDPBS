package com.example.dompedrobarbershop.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;


public class Usuario {
    @SerializedName("COD_FUNCIONARIO")
    @Expose
    private int id;

    @SerializedName("NOME")
    @Expose
    private String nome;

    @SerializedName("EMAIL")
    @Expose
    private String email;

    @SerializedName("SENHA")
    @Expose
    private String senha;


    public Usuario(int id, String nome, String email, String senha){
        this.id = id;
        this.nome = nome;
        this.email = email;
        this.senha = senha;
    }

    public int getId() {return id;}
    public void setId(int id) {this.id =id;}

    public String getNome() {return nome;}
    public void setNome(String nome) {this.nome =nome;}

    public String getEmail() {return email;}
    public void setEmail(String email) {this.email =email;}

    public String getSenha() {return senha;}
    public void setSenha(String senha) {this.senha =senha;}

}
