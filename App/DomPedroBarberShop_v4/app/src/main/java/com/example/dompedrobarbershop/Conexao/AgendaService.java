package com.example.dompedrobarbershop.Conexao;

import com.example.dompedrobarbershop.model.Agenda;

import java.util.List;
import java.util.Map;

import com.example.dompedrobarbershop.model.Agenda;
import com.example.dompedrobarbershop.model.Login;

import retrofit2.Call;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
import retrofit2.http.QueryMap;

public interface AgendaService {

    @GET("agenda/{id}")
    Call<List<Agenda>> getAgenda(@Path("id") int id);

}
