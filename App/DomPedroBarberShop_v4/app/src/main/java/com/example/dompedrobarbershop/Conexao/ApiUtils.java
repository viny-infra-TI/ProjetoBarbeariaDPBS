package com.example.dompedrobarbershop.Conexao;

public class ApiUtils {
private ApiUtils(){

};
   public static final String API_URL = "http://10.0.2.2:8081/api/";



   public static LoginService getLoginService(){
       return RetrofitClient.getClient(API_URL).create(LoginService.class);


   }

    public static AgendaService getAgendaService(){
        return RetrofitClient.getClient(API_URL).create(AgendaService.class);
    }
}
