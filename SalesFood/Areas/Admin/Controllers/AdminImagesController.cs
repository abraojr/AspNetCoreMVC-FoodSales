using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesFood.Models;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminImagesController(IWebHostEnvironment hostEnv, IOptions<ConfigurationImages> myConfig) : Controller
{
    private readonly ConfigurationImages _myConfig = myConfig.Value;

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
        {
            ViewData["Error"] = "Error: File(s) not selected";
            return View(ViewData);
        }

        if (files.Count > 10)
        {
            ViewData["Error"] = "Error: Number of files has exceeded the limit";
            return View(ViewData);
        }

        try
        {

            long size = files.Sum(f => f.Length);
            var filePathsName = new List<string>();
            var filePath = Path.Combine(hostEnv.WebRootPath, _myConfig.NameImagesFolderProducts);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".gif") ||
                    formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using var stream = new FileStream(fileNameWithPath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }
            }

            ViewData["Result"] = $"{files.Count} files have been uploaded to the server, with a total size of : {size} bytes";

            ViewBag.Files = filePathsName;
        }
        catch (Exception ex)
        {
            ViewData["Error"] = $"Error: {ex.Message}";
        }

        return View(ViewData);
    }

    public IActionResult GetImages()
    {
        FileManagerModel model = new();

        try
        {
            var userImagesPath = Path.Combine(hostEnv.WebRootPath, _myConfig.NameImagesFolderProducts);

            DirectoryInfo directory = new(userImagesPath);
            FileInfo[] files = directory.GetFiles();

            model.PathImagesProduct = _myConfig.NameImagesFolderProducts;

            if (files.Length == 0)
            {
                ViewData["Error"] = $"No files found in {userImagesPath} folder";
            }

            model.Files = files;
        }
        catch (Exception ex)
        {
            ViewData["Error"] = $"Error: {ex.Message}";
        }

        return View(model);
    }

    public IActionResult DeleteFile(string fileName)
    {
        try
        {
            string deletedImage = Path.Combine(hostEnv.WebRootPath, _myConfig.NameImagesFolderProducts + "\\", fileName);

            if (System.IO.File.Exists(deletedImage))
            {
                System.IO.File.Delete(deletedImage);
                ViewData["Deleted"] = $"File(s) {deletedImage} successfully deleted";
            }
        }
        catch (Exception ex)
        {
            ViewData["Error"] = $"Error: {ex.Message}";
        }

        return View("Index");
    }
}