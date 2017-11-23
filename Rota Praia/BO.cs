using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace rota_praia1
{
    public class BO
    {
        DAO dao = new DAO();

        #region selects

            public List<Grupo> LerGrupos()
            {
                List<Grupo> lista = dao.LerGrupos();

                return lista;
            }

            public List<Acft> LerAcfts(Grupo grupo)
            {
                List<Acft> lista = dao.LerAcfts(grupo);
                
                return lista;
            }
            
            public List<Local> LerLocais(Grupo grupo)
            {
                List<Local> lista = dao.LerLocais(grupo, null);
                
                return lista;
            }
            
        #endregion

        #region inserts

            public void InserirAlterarGrupo(List<Grupo> grupos)
            {
                dao.InserirAlterarGrupo(grupos);
            }

            public void InserirAlterarGrupo(Grupo grupo)
            {
                dao.InserirAlterarGrupo(grupo);
            }

            public void InserirAlterarAcft(List<Acft> acfts)
            {
                dao.InserirAlterarAcft(acfts);
            }

            public void InserirAlterarAcft(Acft Acft)
            {
                dao.InserirAlterarAcft(Acft);
            }

            public void InserirLocais(List<Local> locais, bool manterConexaoAberta = true)
            {
                int i = 1;
                if (locais != null)
                    foreach (var local in locais)
                    {
                        local.Id = 0;
                        local.Ordem = i++;
                        this.InserirLocal(local, manterConexaoAberta);
                    }
            }
            
            public void InserirLocal(Local local, bool manterConexaoAberta = true)
            {
                if (local != null)
                    dao.InserirLocal(local, manterConexaoAberta);
            }
            
        #endregion

        #region deletes

            public void ExcluirAcfts(List<Acft> acfts)
            {
                foreach (var acft in acfts)
                    dao.ExcluirAcft(acft);   
            }

            public void ExcluirAcft(Acft Acft)
            {
                dao.ExcluirAcft(Acft);
            }

            public void ExcluirLocaisGrupo(Grupo grupo)
            {
                dao.ExcluirLocaisGrupo(grupo);
            }

        #endregion
    }
}