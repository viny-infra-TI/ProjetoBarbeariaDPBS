package com.example.dompedrobarbershop.model;

import  com.google.gson.annotations.Expose;
import  com.google.gson.annotations.SerializedName;

public class Login {

    @SerializedName("EMAIL")
    @Expose
    private String email;

    @SerializedName("SENHA")
    @Expose
    private String senha;

    @SerializedName("ACESSO")
    @Expose
    private String acesso;

    @SerializedName("COD_FUNCIONARIO")
    @Expose
    private String id;

    @SerializedName("NOME")
    @Expose
    private String nome;

    @SerializedName("CPF")
    @Expose
    private String cpf;

    public String getCpf() {return cpf;}
    public void setCpf(String cpf) {this.cpf=id;}

    public String getNome() {return nome;}
    public void setNome(String nome) {this.nome=id;}

    public String getId() {return id;}
    public void setId(String id) {this.id=id;}

    public String getEmail() {return email;}
    public void setEmail(String email) {this.email=id;}

    public String getAcesso() {return acesso;}
    public void setAcesso(String acesso) {this.acesso=id;}

    public String getSenha() {return senha;}
    public void setSenha(String senha) {this.senha=id;}



    public Login(String id, String email, String senha) {
        this.id=id;
        this.email=email;
        this.senha=senha;
    }
}
