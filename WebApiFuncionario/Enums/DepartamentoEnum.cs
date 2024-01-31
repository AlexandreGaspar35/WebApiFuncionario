﻿using System.Text.Json.Serialization;

namespace WebApiFuncionario.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepartamentoEnum
    {
        RH,
        Financeiro,
        Compras,
        Atendimento,
        Zeladoria

    }
}
