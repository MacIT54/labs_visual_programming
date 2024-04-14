using CommunityToolkit.Mvvm.ComponentModel;
using lab11Visual.Entities;

namespace lab11Visual.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private User _userData = new User
    {
        id = 7,
        name = "Daniil Ledin",
        username = "com.macit",
        email = "daniil.ledkin@mail.ru",
        address = new Address()
        {
            street = "B.Bogatkova",
            suite = "194",
            city = "Novosibirsk",
            zipcode = "154154",


            geo = new Geo()
            {
                lat = "66",
                lng = "77",
            }
        },
        phone = "9133681156",
        website = "github.com",
        company = new Company()
        {
            name = "SibSUTIS",
            catchPhrase = "IVT", 
            bs = "214"
        }
    };

    public MainViewModel()
    {
    }
}
