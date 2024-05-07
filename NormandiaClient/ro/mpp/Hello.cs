using System.Windows.Forms;
using System;
using System.Collections.Generic;
using NormandiaService.ro.mpp;
using NormandiaModel.ro.mpp;

namespace NormandiaClient.ro.mpp
{
    public partial class Hello : Form,IRezervareObserver
    {
        private IRezervareServices _service;
        private LogIn _logIn;
        private Client _user;
        public Hello(IRezervareServices service, LogIn logIn)
        {
            this._service = service;
            this._logIn = logIn;
            InitializeComponent();
        }
        public Hello()
        {
            InitializeComponent();
        }//TODO IMPLEMENT CAUTA USER SI SETEAZA ID UL

        private Cursa selectedc=new Cursa();
        
        public void SetUser(string username,string password)
        {
            //List<Client>clients=_service.GetAllUsers();
            foreach (Client user1 in _service.GetAllUsers())
            {
               // System.out.println(user1);
               if (user1.username.Equals(username)&&user1.password.Equals(password))
                {
                    _user=user1;
                    _user.Id = user1.Id;
                    break;
                }
            }

            List<string> curseString=new List<string>();
            foreach (Cursa cursa in _service.GetAllCurse())
            {
                curseString.Add(cursa.ToString());
                comboBox1.Items.Add(cursa.ToString());
            }
            textBoxNume.Text=_user.username;
           // comboBox1.Items.Add()
           init();           
        }

        public void init()
        {
            listBox1.Items.Clear();
            List<Cursa> allCurse = _service.GetAllCurse();
            List<string>curseString=new List<string>();
            Console.WriteLine("TEST1");
            foreach (Cursa cursa in allCurse)
            {
                //listBox1.Items.Add(cursa);
                curseString.Add(cursa.ToString());
                
                List<Rezervare>allRezervari=_service.GetAllRezervari();
                long i = 18;
                
                foreach (Rezervare rezervare in allRezervari)
                {
                    Console.WriteLine("TEST4");
                    if (rezervare.Cursa.Id==cursa.Id)
                    {
                        Console.WriteLine("TEST5");
                        i -= rezervare.locuri;
                    }
                }
                listBox1.Items.Add(cursa.Id + ". Destinatie: " + cursa.destinatie + " | Date: " + cursa.data + " | Available seats:" + i);
            }
            
            listBox1.SelectionMode = SelectionMode.One;
            listBox1.MouseClick += (sender, e) =>
            {
                if (listBox1.SelectedItem != null)
                {
                    string selectedCurse = listBox1.SelectedItem.ToString();
                    // cursaId = selectedCurse.Split('|')[0]; // Extract the selected competition's ID
                   // string destinatie = selectedCurse.Split('|')[0];
                    string date = selectedCurse.Split('|')[1];
                    string availableSeats = selectedCurse.Split('|')[2];
                    int cursaId = int.Parse(selectedCurse.Split('.')[0]);
                    string destinatie = selectedCurse.Split('.')[1].Split('|')[0];
                    string destination = destinatie.Split(' ')[2];
                    string data = date.Split(' ')[2];
                    Console.WriteLine("_"+cursaId+"-");
                    Console.WriteLine(destination);
                    Console.WriteLine(data);
                    Console.WriteLine(availableSeats);
                    selectedc.Id=cursaId;
                    selectedc.destinatie=destination;
                    selectedc.data=data;
                    textBoxDestinatie.Text=destination;
                    textBoxData.Text=data;
                    
                    
                }
            };
           // listBox1.SelectionMode = SelectionMode.MultiExtended;
            //TODO IMPLEMENT WITH LIST 
        }
        private void DisplayParticipants(int id)
        {
            //TODO IMPLEMENT WITH LIST
        }

        public void registerParticipant()
        {
            if (InvokeRequired)
            {
                Console.WriteLine(_user.username );
                BeginInvoke((MethodInvoker)delegate
                {
                    init();
                   // DisplayParticipants(competitionId);
                });
            }
        }


        private void buttonRezervare_Click(object sender, EventArgs e)
        {
            int locuri = int.Parse(textBoxLocuri.Text);
            long LastSeast = 0;
            if (comboBox1.SelectedItem != null)
            {
                string cursaSelected = comboBox1.SelectedItem.ToString();
                //TODO PARSE CURSA
                foreach (Rezervare r in  _service.GetAllRezervari())
                {
                    if(r.Cursa.ToString().Equals(cursaSelected))
                    {
                        LastSeast  += r.locuri;
                    }
                }
                
                if (LastSeast + locuri <= 18)
                {
                   /* Rezervare rezervare = new Rezervare(c, _user, locuri);
                    _service.saveRezervare(rezervare);*/
                    MessageBox.Show("Rezervare efectuata cu succes!");
                    
                    string id=cursaSelected.Split('=')[1];
                    string destinatie = cursaSelected.Split('=')[2];
                    string data = cursaSelected.Split('=')[3];
                    string id1 = id.Split(',')[0];
                    string destinatie1 = destinatie.Split(',')[0];
                    string data1 = data.Split('\'')[1];
                    string destinatieBUN=destinatie1.Split('\'')[1];
                    string dataBUN=data1.Split(':')[0];
                    
                    int idBUN = int.Parse(id1);
                    Console.WriteLine("ID:" + idBUN);
                    Console.WriteLine("Destinatie:"+destinatieBUN);
                    Console.WriteLine("Data:"+dataBUN);
                    Console.WriteLine("Locuri:"+locuri);
                    
                    
                    Cursa rezervareCursa = new Cursa( destinatieBUN, dataBUN);
                    rezervareCursa.Id = idBUN;
                    
                    Rezervare rezervare = new Rezervare(_user, rezervareCursa, locuri);
                    _service.registerRezervare(_user,rezervareCursa,locuri);
                    foreach (Rezervare r in _service.GetAllRezervari())
                    {
                        if(r.Cursa.Equals(rezervareCursa) && r.User.Equals(_user)&&r.locuri==locuri)
                        {
                            LastSeast += 1;
                            for (int i = 0; i < locuri; i++)
                            {
                                Seat seat = new Seat(r, LastSeast);
                                _service.registerSeat(seat);
                                LastSeast += 1;
                            }
                        }
                    }
                    
                    init();
                }
                else
                {
                    MessageBox.Show("Nu sunt suficiente locuri disponibile!");
                }
            }
            else
            {
                Console.WriteLine("Nu a fost selectată nicio valoare!");
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            long LastSeat = 0;
            foreach (Rezervare r in _service.GetAllRezervari())
            {
                if (r.Cursa.Id==selectedc.Id)
                {
                    int locuri = r.locuri;
                    List<Seat> seats = _service.GetAllSeats();
                    foreach (Seat seat in seats)
                    {
                        if (seat.rezervare.Id==r.Id)
                        {
                            listBox2.Items.Add("Seat number: " + seat.seatNumber+" | User: "+r.User.username+" | Destination: "+r.Cursa.destinatie+" | Date: "+r.Cursa.data);
                            LastSeat = seat.seatNumber;
                        }
                    }
                }
            }
            for(long i = LastSeat+1; i <= 18; i++)
            {
                listBox2.Items.Add("Seat number: " + i);
            }

            
        }
    }
}