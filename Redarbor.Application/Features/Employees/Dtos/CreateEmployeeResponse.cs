namespace Redarbor.Application.Features.Employees.Dtos;

public class CreateEmployeeResponse
{
    public Guid EmployeeId { get; set; }

    public int CompanyId { get; set; }

    public int PortalId { get; set; }

    public int StateId { get; set; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public DateTime? LastLogin { get; set; }

    public Guid RoleId { get; set; }

    public string Role { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }
}
