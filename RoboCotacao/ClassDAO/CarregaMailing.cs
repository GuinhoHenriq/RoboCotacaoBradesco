﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Web;
using System.Configuration;
using RoboCotacaoBradesco.ClassModelo;

namespace RoboCotacaoBradesco.ClassDAO
{
    class CarregaMailing
    {
        public List<ClienteCotacao> CarregaMalingCotacao ()
        {
            System.Data.SqlClient.SqlConnection conexao = new SqlConnection("conexao");

            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "STP_CARREGA_MAILING_ROBO_COTACAO";
            comando.CommandTimeout = 3000;
            da = new SqlDataAdapter(comando);
            da.Fill(ds, "Dados");
            conexao.Close();

            List<ClienteCotacao> listaCliente = new List<ClienteCotacao>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                    listaCliente.Add(new ClienteCotacao() { 
                    Cliente_Clicodigo = ds.Tables[0].Rows[i]["CLI_CODIGO"].ToString(),
                    Cliente_CPF_CNPJ = ds.Tables[0].Rows[i]["CPFCNPJ"].ToString(), 
                    Cliente_Nome = ds.Tables[0].Rows[i]["NOME"].ToString(),
                    Cliente_DDD = Convert.ToInt32(ds.Tables[0].Rows[i]["DDD"].ToString()),
                    Cliente_Telefone = Convert.ToInt32(ds.Tables[0].Rows[i]["TELEFONE"].ToString()),
                    Cliente_campanha = ds.Tables[0].Rows[i]["CAMPANHA"].ToString(),
                    Cliente_Matricula = ds.Tables[0].Rows[i]["MATRICULA"].ToString(),
                    Cliente_Agencia = Convert.ToInt32(ds.Tables[0].Rows[i]["AGENCIA"].ToString()),
                    Cliente_Conta = Convert.ToInt32(ds.Tables[0].Rows[i]["CONTA"].ToString()),
                    Cliente_Sucursal = Convert.ToInt32(ds.Tables[0].Rows[i]["SUCURSAL"].ToString()),
                    Cliente_Apolice = Convert.ToInt32(ds.Tables[0].Rows[i]["APOLICE"].ToString()),
                    Cliente_Item = Convert.ToInt32(ds.Tables[0].Rows[i]["ITEM"].ToString()),
                    Cliente_DtFimVigencia = Convert.ToDateTime(ds.Tables[0].Rows[i]["FIM_VIGENCIA"]),
                    Cliente_Email = ds.Tables[0].Rows[i]["EMAIL"].ToString(),
                    Cliente_CEP = Convert.ToInt32(ds.Tables[0].Rows[i]["CEP"].ToString())
                });
                    
                  
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                Process.GetCurrentProcess().Kill();
            }

            return listaCliente;
        }

        public void EnviaEmailCli(ClienteCotacao objCliente)
        {
            System.Data.SqlClient.SqlConnection conexao = new SqlConnection("conexao");
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da;
           
            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "STP_ENVIA_EMAIL_COTACAO";
            comando.CommandTimeout = 3000;
            comando.Parameters.Add("@DESTINATARIO", SqlDbType.VarChar).Value = objCliente.Cliente_Email ;
            comando.Parameters.Add("@CLICODIGO", SqlDbType.VarChar).Value = objCliente.Cliente_Clicodigo;
            comando.Parameters.Add("@FIMVIGEN", SqlDbType.DateTime).Value = objCliente.Cliente_DtFimVigencia;
            comando.Parameters.Add("@SOLICITANTE", SqlDbType.VarChar).Value = objCliente.Cliente_campanha;
            comando.Parameters.Add("@NOMECLI", SqlDbType.VarChar).Value = objCliente.Cliente_Nome;
            da = new SqlDataAdapter(comando);
            da.Fill(ds, "Dados");
            conexao.Close();
        }

        public void AtualizaFlg(ClienteCotacao objCliente)
        {
            System.Data.SqlClient.SqlConnection conexao = new SqlConnection("conexao");
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "STP_FLAG_ENVIO_COTACAO";
            comando.CommandTimeout = 3000;
            comando.Parameters.Add("@CLI_CODIGO", SqlDbType.VarChar).Value = objCliente.Cliente_Clicodigo;
            da = new SqlDataAdapter(comando);
            da.Fill(ds, "Dados");
            conexao.Close();
        }

        public void AtualizaTent(ClienteCotacao objCliente)
        {
            System.Data.SqlClient.SqlConnection conexao = new SqlConnection("conexao");
            SqlCommand comando = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da;

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "STP_TENTATIVA_ENVIO_COTACAO";
            comando.CommandTimeout = 3000;
            comando.Parameters.Add("@CLICODIGO", SqlDbType.VarChar).Value = objCliente.Cliente_Clicodigo;
            da = new SqlDataAdapter(comando);
            da.Fill(ds, "Dados");
            conexao.Close();
        }
    }
}
