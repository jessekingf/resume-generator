# Resume Generator

A tool for generating a resume from JSON data into multiple formats:

1. Markdown
2. HTML
3. PDF

## Prerequisites

The following must be installed to run the application:

1. [.NET 5.0 Runtime](https://dotnet.microsoft.com/download/dotnet/5.0)
2. [Google Chrome](https://www.google.com/chrome/) web browser (required for PDF generation).

## Usage

The application takes two arguments:

1. The source JSON file with the resume data.
2. The directory to place the generated resumes.

```shell
  resume.exe [options] <json resume> <output directory>
```

Options:

```shell
-v, --version  Display the application version
-h, --help     Display the help
```

## Schema

The JSON resume format:

```json
{
  "name": "",
  "label": "",
  "email": "",
  "phone": "",
  "website": "",
  "location": {
    "street": "",
    "city": "",
    "region": "",
    "countryCode": "",
    "postalCode": ""
  },
  "summary": "",
  "highlights": [
    ""
  ],
  "skills": [
    {
      "name": "",
      "keywords": [
        ""
      ]
    }
  ],
  "work": [
    {
      "company": "",
      "position": "",
      "startDate": "",
      "summary": "",
      "location": {
        "street": "",
        "city": "",
        "region": "",
        "countryCode": "",
        "postalCode": ""
      },
      "highlights": [
        "",
      ]
    }
  ],
  "education": [
    {
      "institution": "",
      "area": "",
      "studyType": "",
      "startDate": "",
      "location": {
        "street": "",
        "city": "",
        "region": "",
        "countryCode": "",
        "postalCode": ""
      },
      "highlights": [
        ""
      ]
    }
  ]
}
```

## Example

Example JSON input and generated markdown, HTML, and PDF resumes:

- **JSON** – [JohnDoeResume.json](Example/JohnDoeResume.json)
- **Markdown** – [JohnDoeResume.md](Example/JohnDoeResume.md)
- **HTML** – [JohnDoeResume.html](Example/JohnDoeResume.html)
- **PDF** – [JohnDoeResume.pdf](Example/JohnDoeResume.pdf)
