﻿namespace Resume.Core.Renderers;

using System.Globalization;
using Resume.Core.Model;

/// <summary>
/// Renders plain text resumes with no markup.
/// </summary>
/// <remarks>
/// This should probably use a proper template engine.
/// </remarks>
public class ResumePlainTextRenderer : ResumeTextRenderer
{
    /// <summary>
    /// Renders the resume in markdown.
    /// </summary>
    /// <param name="resume">The resume model.</param>
    /// <returns>The rendered resume markdown text.</returns>
    public override string Render(Resume resume)
    {
        ArgumentNullException.ThrowIfNull(resume);

        this.Clear();

        this.RenderHeader(resume);
        this.RenderSummary(resume.Summary, resume.Highlights);
        this.RenderJobs(resume.Work);
        this.RenderSkills(resume.Skills);
        this.RenderEducation(resume.Education);

        return this.ResumeText;
    }

    private void RenderHeader(Resume r)
    {
        this.AppendLine(r.Name);
        this.AppendLine(r.Label);
        this.AppendLine();
        this.AppendLine($"{r.Email} | {r.Phone}");
        this.AppendLine($"{r.Phone}");

        if (!string.IsNullOrEmpty(r.Website))
        {
            this.AppendLine($"{r.Website}");
        }

        this.AppendLine($"{r.Location.Street}, {r.Location.City}, {r.Location.Region}, {r.Location.PostalCode}");
        this.AppendLine();
    }

    private void RenderSummary(string? summary, IList<string> highlights)
    {
        this.AppendLine("Professional Summary".ToUpper(CultureInfo.CurrentCulture));

        if (!string.IsNullOrEmpty(summary))
        {
            this.AppendLine();
            this.AppendLine(summary);
        }

        this.RenderHighlights(highlights);
    }

    private void RenderJobs(IList<Job> jobs)
    {
        if (jobs.Count == 0)
        {
            return;
        }

        this.AppendLine();
        this.AppendLine("Work Experience".ToUpper(CultureInfo.CurrentCulture));

        foreach (Job job in jobs)
        {
            this.AppendLine();
            this.RenderJob(job);
        }
    }

    private void RenderJob(Job job)
    {
        this.AppendLine($"{job.Position}, {job.Company}");
        this.AppendLine($"{job.Location.City}, {job.Location.Region}");
        this.AppendLine(this.FormatDateRange(job.StartDate, job.EndDate));

        if (!string.IsNullOrEmpty(job.Summary))
        {
            this.AppendLine();
            this.AppendLine(job.Summary);
        }

        this.RenderHighlights(job.Highlights);
    }

    private void RenderSkills(IList<Skill> skills)
    {
        if (skills.Count == 0)
        {
            return;
        }

        this.AppendLine();
        this.AppendLine("Technical Skills".ToUpper(CultureInfo.CurrentCulture));

        foreach (Skill skill in skills)
        {
            this.AppendLine();
            this.Append($"- {skill.Name}");
            if (skill.Keywords.Count > 0)
            {
                this.Append($" – {string.Join(", ", skill.Keywords)}");
            }
        }

        this.AppendLine();
    }

    private void RenderEducation(IList<EducationProgram> programs)
    {
        if (programs.Count == 0)
        {
            return;
        }

        this.AppendLine();
        this.AppendLine("Education".ToUpper(CultureInfo.CurrentCulture));

        foreach (EducationProgram program in programs)
        {
            this.AppendLine();
            this.RenderProgram(program);
        }
    }

    private void RenderProgram(EducationProgram program)
    {
        this.AppendLine($"{program.Area}**, **{program.StudyType}**, _**{program.Institution}**_");
        this.AppendLine($"{program.Location.City}, {program.Location.Region}");
        this.AppendLine(this.FormatDateRange(program.StartDate, program.EndDate));
        this.RenderHighlights(program.Highlights);
    }

    private void RenderHighlights(IList<string> highlights)
    {
        if (highlights.Count == 0)
        {
            return;
        }

        this.AppendLine();

        foreach (string highlight in highlights)
        {
            this.AppendLine($"- {highlight}");
        }
    }
}
