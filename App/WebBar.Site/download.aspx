<%@ Page Language="C#" AutoEventWireup="true" %>

<script language="C#" runat="server"> 

    void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            return;

        var strRequest = Request.QueryString["file"];
        if (!string.IsNullOrEmpty(strRequest))
        {
            var file = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(strRequest));

            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + file.Name +
                                                            "\"; filename*=utf-8''"
                                                            + HttpUtility.UrlEncode(file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString(
                    System.Globalization.CultureInfo.InvariantCulture));
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                Response.Write("Файл не существует.");
            }
        }
        else
        {
            Response.Write("Не указан файл для загрузки.");
        }
    }
</script>
