using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;

namespace EIRA.Application.Mappings.Transforms
{
    public static class IssuesIncomingFileTransform
    {
        public static IssueCreateRequest ToIssueCreateRequest(this IssuesIncomingFile source)
        {
            var output = new IssueCreateRequest
            {
                //Customfield10068 = "123456",
                Assignee = new Assignee
                {
                    Id = "557058:8032e131-f9c8-4080-a44e-1e707323b32a"
                },
                Description = new Description
                {
                    Content = new List<DescriptionContent> 
                    {
                        new DescriptionContent
                        {
                            Type = "paragraph",
                            Content = new List<ContentContent> { 
                                new ContentContent
                                {
                                    Text= source.Summary,
                                    Type = "text",
                                }
                            }
                        }
                    },
                    Type = "doc",
                    Version = 1
                },
                Project = new Assignee
                {
                    Id = "10000"
                },
                Summary = source.Summary,
                Issuetype = new Assignee
                {
                    Id = "10009"
                },
                Priority = new Priority
                {
                    Name = "High"
                }
            };

            return output;
        }
    }
}
