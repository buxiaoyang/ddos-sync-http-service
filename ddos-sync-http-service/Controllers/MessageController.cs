using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aspnetapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        // GET: api/Message/5
        [HttpGet("{id}", Name = "Get")]
        public string[] Get(string id)
        {
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", Common.CommonFunc.escapePath(id));
            if(Directory.Exists(rootPath))
            {
                string[] filePaths = Directory.GetFiles(rootPath, "*.txt");
                List<string> messages = new List<string>();
                foreach (string filePath in filePaths)
                {
                    messages.Add(System.IO.File.ReadAllText(filePath, System.Text.Encoding.UTF8));
                    System.IO.File.Delete(filePath);
                }

                return messages.ToArray();
            }
            else
            {
                return new string[] { };
            }
        }

        // POST: api/Message
        [HttpPost]
        public void Post(SendMessage sendMessage)
        {
            string rootPath = Directory.GetCurrentDirectory();
            string[] recipients = sendMessage.recipients.Split(',');
            foreach(string recipient in recipients)
            {
                string filePath = Path.Combine(rootPath, "Data", Common.CommonFunc.escapePath(recipient));
                if(!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                System.IO.File.WriteAllTextAsync(Path.Combine(filePath, DateTime.Now.ToString("yyyyMMdd_HHmmss_fffffff") + ".txt"), sendMessage.message, System.Text.Encoding.UTF8);
            }
        }

    }
}
