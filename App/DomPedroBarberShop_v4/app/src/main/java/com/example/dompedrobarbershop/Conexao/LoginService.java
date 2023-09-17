package com.example.dompedrobarbershop.Conexao;

import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;


import com.example.dompedrobarbershop.model.Login;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.PATCH;
import retrofit2.http.PUT;
import retrofit2.http.Part;
import retrofit2.http.Path;
import retrofit2.http.Query;
import retrofit2.http.QueryMap;


public interface LoginService {
    @GET("Login/")
     Call<List<Login>> getLogin(
            @QueryMap Map<String, String> filters
    );
}
