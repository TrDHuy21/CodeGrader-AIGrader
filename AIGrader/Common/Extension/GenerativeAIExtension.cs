using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.AspNetCore.Http;

namespace Common.Extension
{
    public static class GenerativeAIExtension
    {
        public static void AddFile(this IContentsRequest request, IFormFile file)
        {
            if (file == null)
                return;
            using var reader = new StreamReader(file.OpenReadStream());
            string fileName = file.FileName;
            var content = reader.ReadToEnd();
            var text = $"{fileName}:\n{content}";
            request.AddText(content);
        }

        public static void AddMultiFile(this IContentsRequest request, List<IFormFile> files)
        {
            if(files==null || !files.Any()) 
                return;

            foreach (var item in files)
            {
                request.AddFile(item);
            }
        }

    }
}
