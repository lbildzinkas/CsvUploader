using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace CsvUploader.Api.Providers
{
    public interface IMultipartRequestUtilitiesProvider
    {
        string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit);
        bool IsMultipartContentType(string contentType);
        bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition);
        bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition);
        Encoding GetEncoding(MultipartSection section);
        MultipartReader CreateMultipartReader(string boundary, Stream stream);
    }
}