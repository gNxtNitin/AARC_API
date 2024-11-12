using AALib;
using AgencyAdmins.Helper;
using AgencyAdmins.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace AgencyAdmins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [Route("INVTypes")]
        [HttpGet]
        public async Task<string> getINVTypes()
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_get_types", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("INVDescs/{Type}")]
        [HttpGet]
        public async Task<string> getINVDescs(string Type)
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sType", string.IsNullOrEmpty(Type) ? "" : Type));

                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_get_descs", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("INVManus")]
        [HttpGet]
        public async Task<string> getINVManus(string sType, string sDesc)
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sType", string.IsNullOrEmpty(sType) ? "" : sType));
                    lstParams.Add(new SqlParameter("@sDesc", string.IsNullOrEmpty(sDesc) ? "" : sDesc));
                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_get_manus", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }


        [Route("INVModels")]
        [HttpGet]
        public async Task<string> getINVModels(string sType, string sDesc, string sManu)
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sType", string.IsNullOrEmpty(sType) ? "" : sType));
                    lstParams.Add(new SqlParameter("@sDesc", string.IsNullOrEmpty(sDesc) ? "" : sDesc));
                    lstParams.Add(new SqlParameter("@sManu", string.IsNullOrEmpty(sManu) ? "" : sManu));
                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_get_models", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("addInvItem")]
        [HttpPost]

        public async Task<string> addInvItem(Inventory inventory)
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@sInvType", inventory.InvType));
                    lstParams.Add(new SqlParameter("@sInvDesc", inventory.InvDesc));
                    lstParams.Add(new SqlParameter("@sInvManu", string.IsNullOrEmpty(inventory.InvManu) ? "" : inventory.InvManu));
                    lstParams.Add(new SqlParameter("@sInvModel", string.IsNullOrEmpty(inventory.InvModel) ? "" : inventory.InvModel));
                    lstParams.Add(new SqlParameter("@sInvSerialNo", string.IsNullOrEmpty(inventory.InvSerialNo) ? "" : inventory.InvSerialNo));
                    lstParams.Add(new SqlParameter("@bInvOwn", (inventory.InvOwn == "OWN" ? 1 : 0)));
                    lstParams.Add(new SqlParameter("@sInvVendor", string.IsNullOrEmpty(inventory.InvVendor) ? "" : inventory.InvVendor));
                    lstParams.Add(new SqlParameter("@sInvPrice", string.IsNullOrEmpty(inventory.InvPrice) ? "" : inventory.InvPrice));
                    lstParams.Add(new SqlParameter("@dInvAquireDT", string.IsNullOrEmpty(inventory.InvAquireDT) ? "" : inventory.InvAquireDT));
                    lstParams.Add(new SqlParameter("@sInvIP", string.IsNullOrEmpty(inventory.InvIP) ? "" : inventory.InvIP));
                    lstParams.Add(new SqlParameter("@sInvPossessor", string.IsNullOrEmpty(inventory.InvPossessor) ? "" : inventory.InvPossessor));
                    lstParams.Add(new SqlParameter("@sInvNotes", string.IsNullOrEmpty(inventory.InvNotes) ? "" : inventory.InvNotes));

                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_add", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("updInvItem")]
        [HttpPut]
        public async Task<string> updInvItem(Inventory inventory)
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@iInvID", inventory.InvID));
                    lstParams.Add(new SqlParameter("@sInvType", inventory.InvType));
                    lstParams.Add(new SqlParameter("@sInvDesc", inventory.InvDesc));
                    lstParams.Add(new SqlParameter("@sInvManu", string.IsNullOrEmpty(inventory.InvManu) ? "" : inventory.InvManu));
                    lstParams.Add(new SqlParameter("@sInvModel", string.IsNullOrEmpty(inventory.InvModel) ? "" : inventory.InvModel));
                    lstParams.Add(new SqlParameter("@sInvSerialNo", string.IsNullOrEmpty(inventory.InvSerialNo) ? "" : inventory.InvSerialNo));
                    lstParams.Add(new SqlParameter("@bInvOwn", (inventory.InvOwn == "OWN" ? 1 : 0)));
                    lstParams.Add(new SqlParameter("@sInvVendor", string.IsNullOrEmpty(inventory.InvVendor) ? "" : inventory.InvVendor));
                    lstParams.Add(new SqlParameter("@sInvPrice", string.IsNullOrEmpty(inventory.InvPrice) ? "" : inventory.InvPrice));
                    lstParams.Add(new SqlParameter("@dInvAquireDT", string.IsNullOrEmpty(inventory.InvAquireDT) ? "" : inventory.InvAquireDT));
                    lstParams.Add(new SqlParameter("@sInvIP", string.IsNullOrEmpty(inventory.InvIP) ? "" : inventory.InvIP));
                    lstParams.Add(new SqlParameter("@sInvPossessor", string.IsNullOrEmpty(inventory.InvPossessor) ? "" : inventory.InvPossessor));
                    lstParams.Add(new SqlParameter("@sInvNotes", string.IsNullOrEmpty(inventory.InvNotes) ? "" : inventory.InvNotes));

                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_upd", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("InvItems")]
        [HttpGet]
        public async Task<string> getInvItems(int iArchive, int iOwn)

        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@iArchived", iArchive));
                    lstParams.Add(new SqlParameter("@iOwn", iOwn));
                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_get_all_by_archown", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("InvItemByID/{iInvID}")]
        [HttpGet]
        public async Task<string> getInvItemByID(int iInvID)

        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@iInvID", iInvID));
                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_get_by_id", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

        [Route("updInvItemArchive/{iInvID}/{bInvArchive}")]
        [HttpGet]
        public async Task<string> updInvItemArchive(string sInvID, bool bInvArchive)
        {
            {
                AALib.clsAA oLib = new AALib.clsAA();
                DataTable dtRet = null;
                try
                {
                    List<SqlParameter> lstParams = new List<SqlParameter>();
                    lstParams.Add(new SqlParameter("@iInvID", sInvID));
                    lstParams.Add(new SqlParameter("@bInvArchive", bInvArchive));


                    dtRet = oLib.AAExecuteTable(clsAA.validDBs.AARC, "aarc_inv_archive", lstParams);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    new LogException().logAARCErr(ex, "");
                }
                finally
                {
                    oLib = null;
                }

                return JsonConvert.SerializeObject(dtRet);
            }
        }

    }
}
