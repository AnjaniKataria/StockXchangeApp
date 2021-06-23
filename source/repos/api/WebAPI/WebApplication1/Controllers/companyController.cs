using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class companyController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                           select company_name,sector_name,brief_writeup 
                           from dbo.company
                           ";
            DataTable table = new DataTable();
            using(var con=new SqlConnection(ConfigurationManager.
                ConnectionStrings["CompanyAppDB"].ConnectionString))
                using (var cmd=new SqlCommand(query,con))
            using(var da=new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

            public string Post(company dep)
        {

            try
            {
                string query = @"
                                insert into dbo.company values
                               ('" +dep.company_code+@"',
                                 '"+dep.company_name+@"',
                                  '" + dep.turnover+@"',
                                  '" + dep.ceo + @"',
                                  '" + dep.board_of_director + @"',
                                  '" + dep.listed_in_stock_exchange + @"',
                                  '" + dep.sector_name + @"',
                                  '" + dep.brief_writeup+@"',
                                  '"+dep.stock_code+ @"')
                               ";
                 
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["CompanyAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully";
            }
            catch(Exception)
            {
                return "Failed to add!!!";
            }
        }

        public string Put(company dep)
        {
            try
            {
                string query = @"
                               update dbo.company set 
                               company_name=
                               '" + dep.company_name + @"'
                               where company_code="+dep.company_code+@"
                               ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["CompanyAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to update!!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                               delete from dbo.company 
                               where company_code=
                               " + id + @"
                               ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["CompanyAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to delete!!!";
            }
        }

        [Route("api/company/GetAllCompanyDetails")]
        [HttpGet]
        public HttpResponseMessage GetAllCompanyDetails()
        {
            string query = @"
                           select company_name,sector_name,brief_writeup from dbo.company
                            ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["CompanyAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK,table);
        }
            
        

        [Route("api/company/SaveFile")]
        //[HttpPost]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch(Exception)
            {
                return "Failed!!!";
            }
        }
    }
}
