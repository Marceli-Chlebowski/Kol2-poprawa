﻿namespace WebApplication1.DTO_s;

public class OwnerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public List<OwnedObjectDto> OwnerObjects { get; set; }
}