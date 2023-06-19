using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BlazorAppNetwork.Data;

public class CarDataService
{
    private string conString = "Data Source =(LocalDB)\\MSSQLLocalDB;  initial catalog=CarDB;TrustServerCertificate=True;User ID=sa; Password=Passw0rd";

    public CarData ReadCarData()
    {
        return new CarData(1,"Fiat","Punto","2002", "10999.99");
    }
    public List<CarData> ReadCarData(string query)
    {
        List<CarData> list = new();
        using (SqlConnection con = new(conString)) 
        {
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CarData carData = new CarData();
                carData.Id = (int)reader[0];
                carData.Brand = (string)reader[1];
                carData.Model = (string)reader[2];
                carData.Year = (string)reader[3];
                carData.Price = (string)reader[4];
                list.Add(carData);
            }
            con.Close();
        }
        return list;
    }
    public void CreateCar(CarData CD)
    {
        using (SqlConnection con = new(conString))
        {
            string query = $"INSERT INTO CarData([Brand], model, year, price) VALUES(@Brand,@Model, @Year, @Price)";
            SqlCommand cmd = new SqlCommand(query, con);
            if (CD.Brand == null) CD.Brand = "Brand not given";
            cmd.Parameters.Add("@Brand", System.Data.SqlDbType.NVarChar).Value = CD.Brand;
            if (CD.Model == null) CD.Model = "Model not given";
            cmd.Parameters.Add("@Model", System.Data.SqlDbType.NVarChar).Value = CD.Model;
            if (CD.Year == null) CD.Year = "Year not specified";
            cmd.Parameters.Add("@Year", System.Data.SqlDbType.NVarChar).Value = CD.Year;
            if (CD.Price == null) CD.Price = "Price not given";
            cmd.Parameters.Add("@Price", System.Data.SqlDbType.NVarChar).Value = CD.Price;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    public void DeleteCar(int Id)
    {
        using (SqlConnection con = new(conString))
        {
            string query = $"DELETE FROM CarData WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand (query, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            cmd.ExecuteNonQuery ();
            con.Close();
        }
    }
}
