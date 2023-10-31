using System.ComponentModel;

namespace Full_CRUD_Zoo;

public class Animal
{
  public int AnimalID { get; set; }
  public string Name { get; set; }
  public string Species { get; set; }
  public string Diet { get; set; }
  public string Photo { get; set; }
  [DisplayName("Date Of Birth")]
  public DateTime DateOfBirth { get; set; }
  [DisplayName("Date Aquired")]
  public DateTime DateAquired { get; set; }
  [DisplayName("Last Fed")]
  public DateTime LastFed { get; set; }

}
