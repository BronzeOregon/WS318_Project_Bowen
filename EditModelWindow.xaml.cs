using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for EditModelWindow.xaml
    /// </summary>
    public partial class EditModelWindow : Window
    {
        NonVehicleModel nvmSelection = new NonVehicleModel();

        VehicleModel vmSelection = new VehicleModel();

        SpecialRule selection = new SpecialRule();

        RangedWeapon selectedRange = new RangedWeapon();

        MeleeWeapon selectedMelee = new MeleeWeapon();

        ObservableCollection<SpecialRule> AddedRules = new ObservableCollection<SpecialRule>();

        ObservableCollection<RangedWeapon> AddedRange = new ObservableCollection<RangedWeapon>();

        ObservableCollection<MeleeWeapon> AddedMelee = new ObservableCollection<MeleeWeapon>();

        public EditModelWindow()
        {
            InitializeComponent();
            setDataContext();
        }

        public void setDataContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var allRules = context.SpecialRules.ToList();
                SpecialRuleGrid.ItemsSource = allRules;
            }
        }

        public void setWeaponContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var meleeWeps = context.MeleeWeapons.ToList();
                MeleeWeaponGrid.ItemsSource = meleeWeps;
                var rangedWeps = context.RangedWeapons.ToList();
                RangedWeaponGrid.ItemsSource = rangedWeps;
            }
        }

        public void setNVModelContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var nvm = context.NonVehicleModels.ToList();
                ModelGrid.ItemsSource = nvm;
            }
        }

        public void setVModelContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var vm = context.VehicleModels.ToList();
                ModelGrid.ItemsSource = vm;
            }
        }

        public void setAddedContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                AddedRulesGrid.ItemsSource = AddedRules;
                AddedRangedWeaponsGrid.ItemsSource = AddedRange;
                AddedMeleeWeaponsGrid.ItemsSource = AddedMelee;
            }
        }

        private void NonVehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            VehicleCheck.IsChecked = false;
            VehicleEntry.Visibility = Visibility.Collapsed;
            NonVehicleEntry.Visibility = Visibility.Visible;
            MeleeWeaponGrid.Visibility = Visibility.Visible;
            MeleeWeaponGrid.Width = this.Width / 2 - 20;
            RangedWeaponGrid.Width = this.Width / 2 - 20;
            AddedMeleeWeaponsGrid.Visibility = Visibility.Visible;
            AddedMeleeWeaponsGrid.Width = this.Width / 2 - 20;
            AddedRangedWeaponsGrid.Width = this.Width / 2 - 20;
            setNVModelContext();
            setWeaponContext();
        }

        private void VehicleCheck_Checked(object sender, RoutedEventArgs e)
        {
            NonVehicleCheck.IsChecked = false;
            NonVehicleEntry.Visibility = Visibility.Collapsed;
            VehicleEntry.Visibility = Visibility.Visible;
            MeleeWeaponGrid.Visibility = Visibility.Collapsed;
            AddedMeleeWeaponsGrid.Visibility = Visibility.Collapsed;
            RangedWeaponGrid.Width = this.Width - 20;
            AddedRangedWeaponsGrid.Width = this.Width - 20;
            setVModelContext();
            setWeaponContext();
        }

        private void ModelSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                string search = ModelSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.NonVehicleModels.Where(s => s.Name.StartsWith(search));
                    ModelGrid.ItemsSource = filteredEntities.ToList();
                                        
                }
                if (ModelSearchBar.Text.Equals("") || ModelSearchBar.Text.IsNullOrEmpty())
                {
                    setNVModelContext();
                }
            }
            else if (VehicleCheck.IsChecked == true)
            {
                string search = ModelSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.VehicleModels.Where(s => s.Name.StartsWith(search));
                    ModelGrid.ItemsSource = filteredEntities.ToList();
                }
                if (ModelSearchBar.Text.Equals("") || ModelSearchBar.Text.IsNullOrEmpty())
                {
                    setVModelContext();
                }
            }
        }

        private void PickModel_Click(object sender, RoutedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    try
                    {
                        if (ModelGrid.SelectedItem != null)
                        {
                            nvmSelection = (NonVehicleModel)ModelGrid.SelectedItem;
                            
                            ModelName.Text = nvmSelection.Name;
                            MValue.Text = nvmSelection.M.ToString();
                            WSValue.Text = nvmSelection.Ws.ToString();
                            BSValue.Text = nvmSelection.Bs.ToString();
                            SValue.Text = nvmSelection.S.ToString();
                            TValue.Text = nvmSelection.T.ToString();
                            WValue.Text = nvmSelection.W.ToString();
                            IValue.Text = nvmSelection.I.ToString();
                            AValue.Text = nvmSelection.A.ToString();
                            LdValue.Text = nvmSelection.Ld.ToString();
                            ClValue.Text = nvmSelection.Cl.ToString();
                            WpValue.Text = nvmSelection.Wp.ToString();
                            IntValue.Text = nvmSelection.Int.ToString();
                            SvValue.Text = nvmSelection.Sv;
                            InvValue.Text = nvmSelection.Inv;
                            TypeValue.Text = nvmSelection.Type;
                            TraitsValue.Text = nvmSelection.Traits;

                            AddedRange.Clear();

                            AddedRules.Clear();

                            AddedMelee.Clear();

                            IEnumerable<NonVehicleModelRangedWeapon> nvrw =
                                from nvr in context.NonVehicleModelRangedWeapons
                                where nvr.NonVehicleModelId == nvmSelection.Id
                                select nvr;

                            List<int> rwIds = new List<int>();

                            foreach(NonVehicleModelRangedWeapon r in nvrw)
                            {
                                rwIds.Add(r.RangedWeaponId);
                            }

                            IEnumerable<RangedWeapon> rw;

                            foreach (var nvr in nvrw.ToList())
                            {
                                rw =
                                    from r in context.RangedWeapons
                                    where r.Id.Equals(nvr.RangedWeaponId)
                                    select r;


                                foreach (RangedWeapon r in rw)
                                {
                                    AddedRange.Add(r);
                                    Debug.WriteLine("AddedRW.");
                                }
                            }

                                                                                   
                            IEnumerable<NonVehicleModelMeleeWeapon> nvmw =
                                from nvm in context.NonVehicleModelMeleeWeapons
                                where nvm.NonVehicleModelId == nvmSelection.Id
                                select nvm;

                            List<int> mwIds = new List<int>();

                            foreach(NonVehicleModelMeleeWeapon m in nvmw)
                            {
                                mwIds.Add(m.MeleeWeaponIdtoModel);
                            }

                            IEnumerable<MeleeWeapon> mw;

                            foreach (var nvm in nvmw.ToList())
                            {
                                mw =
                                    from m in context.MeleeWeapons
                                    where m.Id.Equals(nvm.MeleeWeaponIdtoModel)
                                    select m;


                                foreach (MeleeWeapon m in mw)
                                {
                                    AddedMelee.Add(m);
                                    Debug.WriteLine("AddedMW.");
                                }
                            }


                            IEnumerable<NonVehicleModelSpecialRule> nvsr =
                                from nvs in context.NonVehicleModelSpecialRules
                                where nvs.NonVehicleModelId == nvmSelection.Id
                                select nvs;

                            List<int> srIds = new List<int>();

                            foreach(NonVehicleModelSpecialRule s in nvsr)
                            {
                                srIds.Add(s.SpecialRulesId);
                            }

                            IEnumerable<SpecialRule> sr;

                            foreach (var nvs in nvsr.ToList())
                            {
                                sr =
                                    from s in context.SpecialRules
                                    where s.Id.Equals(nvs.SpecialRulesId)
                                    select s;

                                

                                foreach(SpecialRule s in sr)
                                {
                                    AddedRules.Add(s);
                                    Debug.WriteLine("AddedSR.");
                                }
                            }

                            setAddedContext();
                        }

                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }

            }
            else if (VehicleCheck.IsChecked == true)
            {

                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    try
                    {
                        if (ModelGrid.SelectedItem != null)
                        {
                            vmSelection = (VehicleModel)ModelGrid.SelectedItem;

                            ModelName.Text = vmSelection.Name;
                            VMValue.Text = vmSelection.M.ToString();
                            VBSValue.Text = vmSelection.Bs.ToString();
                            FrontAVValue.Text = vmSelection.FrontAv.ToString();
                            SideAVValue.Text= vmSelection.SideAv.ToString();
                            RearAVValue.Text = vmSelection.RearAv.ToString();
                            HPValue.Text = vmSelection.Hp.ToString();
                            CapacityValue.Text = vmSelection.Capacity.ToString();
                            VTypesValue.Text = vmSelection.Type;
                            VTraitsValue.Text = vmSelection.Traits;

                            AddedRange.Clear();

                            AddedRules.Clear();

                            AddedMelee.Clear();

                            IEnumerable<VehicleModelRangedWeapon> nvrw = 
                                from nvr in context.VehicleModelRangedWeapons
                                where nvr.VehicleModelId == vmSelection.Id
                                select nvr;

                            List<int> rwIds = new List<int>();

                            foreach (VehicleModelRangedWeapon r in nvrw)
                            {
                                rwIds.Add(r.RangedWeaponId);
                            }

                            IEnumerable<RangedWeapon> rw;

                            foreach (var nvr in nvrw.ToList())
                            {
                                rw =
                                    from r in context.RangedWeapons
                                    where r.Id.Equals(nvr.RangedWeaponId)
                                    select r;


                                foreach (RangedWeapon r in rw)
                                {
                                    AddedRange.Add(r);
                                    Debug.WriteLine("AddedRW.");
                                }
                            }

                            IEnumerable<VehicleModelSpecialRule> vmsr =
                                from v in context.VehicleModelSpecialRules
                                where v.VehicleModelId == vmSelection.Id
                                select v;

                            List<int> srIds = new List<int>();

                            foreach (VehicleModelSpecialRule s in vmsr)
                            {
                                srIds.Add(s.SpecialRulesId);
                            }

                            IEnumerable<SpecialRule> sr;

                            foreach (var nvs in vmsr.ToList())
                            {
                                sr =
                                    from s in context.SpecialRules
                                    where s.Id.Equals(nvs.SpecialRulesId)
                                    select s;



                                foreach (SpecialRule s in sr)
                                {
                                    AddedRules.Add(s);
                                    Debug.WriteLine("AddedSR.");
                                }
                            }

                            setAddedContext();

                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }

            }
            else
            {
                ConfirmationLabel.Content = "ERROR, PLEASE SELECT A MODEL TYPE AND MODEL.";
            }
        }

        private void ModelGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBar.Text.Equals("") || SearchBar.Text.IsNullOrEmpty())
            {
                setDataContext();
            }
            else
            {
                string search = SearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.SpecialRules.Where(s => s.Name.StartsWith(search));
                    SpecialRuleGrid.ItemsSource = filteredEntities.ToList();
                }
            }
        }

        private void AddSpecialRule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddedRules.Add(selection);
                AddedRulesGrid.ItemsSource = AddedRules;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddedRulesGrid.SelectedItem != null)
            {
                selection = (SpecialRule)AddedRulesGrid.SelectedItem;
                try
                {
                    foreach (SpecialRule a in AddedRules)
                    {
                        if (a.Equals(selection.Id))
                        {
                            selection = a;
                            Debug.WriteLine("Match!");
                        }
                    }

                    AddedRules.Remove(selection);
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
        }

        private void SpecialRuleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { selection = (SpecialRule)SpecialRuleGrid.SelectedItem; }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void AddedRulesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selection = (SpecialRule)AddedRulesGrid.SelectedItem;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void WeaponSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                string search = WeaponSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.RangedWeapons.Where(s => s.Name.StartsWith(search));
                    RangedWeaponGrid.ItemsSource = filteredEntities.ToList();

                    var mfilteredEntities = context.MeleeWeapons.Where(s => s.Name.StartsWith(search));
                    MeleeWeaponGrid.ItemsSource = mfilteredEntities.ToList();
                }
                if (WeaponSearchBar.Text.Equals("") || WeaponSearchBar.Text.IsNullOrEmpty())
                {
                    setWeaponContext();
                }
            }
            else if (VehicleCheck.IsChecked == true)
            {
                string search = WeaponSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.RangedWeapons.Where(s => s.Name.StartsWith(search));
                    RangedWeaponGrid.ItemsSource = filteredEntities.ToList();
                }
                if (WeaponSearchBar.Text.Equals("") || WeaponSearchBar.Text.IsNullOrEmpty())
                {
                    setWeaponContext();
                }
            }
        }

        private void PickWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (NonVehicleCheck.IsChecked == true)
            {
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    try
                    {
                        if (RangedWeaponGrid.SelectedItem != null)
                        {
                            selectedRange = (RangedWeapon)RangedWeaponGrid.SelectedItem;
                            AddedRange.Add(selectedRange);
                            AddedRangedWeaponsGrid.ItemsSource = AddedRange;
                        }

                        if (MeleeWeaponGrid.SelectedItem != null)
                        {
                            selectedMelee = (MeleeWeapon)MeleeWeaponGrid.SelectedItem;
                            AddedMelee.Add(selectedMelee);
                            AddedMeleeWeaponsGrid.ItemsSource = AddedMelee;

                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }

            }
            else if (VehicleCheck.IsChecked == true)
            {

                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    try
                    {
                        selectedRange = (RangedWeapon)RangedWeaponGrid.SelectedItem;
                        AddedRange.Add(selectedRange);
                        AddedRangedWeaponsGrid.ItemsSource = AddedRange;
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }

            }
            else
            {
                ConfirmationLabel.Content = "Error adding weapons.";
            }
        }

        private void DeleteWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddedRangedWeaponsGrid.SelectedItem != null)
            {
                try
                {
                    foreach (RangedWeapon a in AddedRange)
                    {
                        if (a.Equals(selectedRange.Id))
                        {
                            selectedRange = a;
                        }
                    }

                    AddedRange.Remove(selectedRange);
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
            else if (AddedMeleeWeaponsGrid.SelectedItem != null)
            {
                try
                {
                    foreach (MeleeWeapon a in AddedMelee)
                    {
                        if (a.Equals(selectedMelee.Id))
                        {
                            selectedMelee = a;
                        }

                        AddedMelee.Remove(selectedMelee);
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
        }

        private void RangedWeaponGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MeleeWeaponGrid.SelectedItem = null;
        }

        private void MeleeWeaponGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RangedWeaponGrid.SelectedItem = null;
        }

        private void AddedRangedWeaponsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddedMeleeWeaponsGrid.SelectedItem = null;
            selectedRange = (RangedWeapon)AddedRangedWeaponsGrid.SelectedItem;
        }

        private void AddedMeleeWeaponsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddedRangedWeaponsGrid.SelectedItem = null;
            selectedMelee = (MeleeWeapon)AddedMeleeWeaponsGrid.SelectedItem;
        }

        private void SaveModelButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                if (NonVehicleCheck.IsChecked == true)
                {

                    NonVehicleModel nvm = new NonVehicleModel();
                    
                    nvm.Name = ModelName.Text;
                    nvm.M = int.Parse(MValue.Text);
                    nvm.Ws = int.Parse(WSValue.Text);
                    nvm.Bs = int.Parse(BSValue.Text);
                    nvm.S = int.Parse(SValue.Text);
                    nvm.T = int.Parse(TValue.Text);
                    nvm.W = int.Parse(WValue.Text);
                    nvm.I = int.Parse(IValue.Text);
                    nvm.A = int.Parse(AValue.Text);
                    nvm.Ld = int.Parse(LdValue.Text);
                    nvm.Cl = int.Parse(ClValue.Text);
                    nvm.Wp = int.Parse(WpValue.Text);
                    nvm.Int = int.Parse(IntValue.Text);
                    nvm.Sv = SvValue.Text;
                    nvm.Inv = InvValue.Text;
                    nvm.Type = TypeValue.Text;
                    nvm.Traits = TraitsValue.Text;

                    try
                    {

                        //Update selected model to have new characteristics, then update special rules.
                        nvm.Id = nvmSelection.Id;
                        var NonVehicleModel = nvm;

                        context.NonVehicleModels.Update(nvm);
                        context.SaveChanges();
                        ConfirmationLabel.Content = nvm.Name + " added Successfully!";


                        //Check for existing Special Rules and associated M to M relationship entities, then remove or create as needed.

                        foreach (SpecialRule s in AddedRules)
                        {
                            nvm = context.NonVehicleModels.Find(nvm.Id);
                            if (nvm != null)
                            {
                                IQueryable<NonVehicleModelSpecialRule> RWSWSearch =
                                    (from n in context.NonVehicleModelSpecialRules
                                     where n.NonVehicleModelId == nvm.Id
                                     select n);


                                var RWSR = new NonVehicleModelSpecialRule();
                                RWSR.Id = context.NonVehicleModelSpecialRules.Count() + 1;
                                RWSR.NonVehicleModelId = nvm.Id;

                                List<int> RWSWIds = new List<int>();

                                foreach (NonVehicleModelSpecialRule a in RWSWSearch)
                                {
                                    if (a.NonVehicleModelId == RWSR.NonVehicleModelId && a.NonVehicleModelId == RWSR.NonVehicleModelId)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        RWSWIds.Add(a.Id);
                                    }

                                }

                                if (!RWSWIds.Contains(RWSR.Id))
                                {
                                    context.NonVehicleModelSpecialRules.Add(RWSR);
                                    context.SaveChanges();
                                }
                            }

                        }

                        List<int> AddedRuleIDs = new List<int>();

                        foreach (SpecialRule a in AddedRules)
                        {
                            AddedRuleIDs.Add(a.Id);
                        }

                        IQueryable<NonVehicleModelSpecialRule> obsoleteRWSW =
                            (from rangedweaponspecialrule in context.NonVehicleModelSpecialRules
                             where rangedweaponspecialrule.NonVehicleModelId == NonVehicleModel.Id && !AddedRuleIDs.Contains(rangedweaponspecialrule.SpecialRulesId)
                             select rangedweaponspecialrule);
                        if (obsoleteRWSW != null)
                        {
                            foreach (NonVehicleModelSpecialRule a in obsoleteRWSW)
                            {
                                context.NonVehicleModelSpecialRules.Remove(a);
                            }

                            context.SaveChanges();
                        }

                        //Check for existing Ranged Weapons and associated M to M relationship entities, then remove or create as needed.

                        foreach (RangedWeapon s in AddedRange)
                        {
                            nvm = context.NonVehicleModels.Find(nvm.Id);
                            if (nvm != null)
                            {
                                IQueryable<NonVehicleModelRangedWeapon> RWSWSearch =
                                    (from n in context.NonVehicleModelRangedWeapons
                                     where n.NonVehicleModelId == nvm.Id
                                     select n);


                                var RWSR = new NonVehicleModelRangedWeapon();
                                RWSR.Id = context.NonVehicleModelRangedWeapons.Count() + 1;
                                RWSR.NonVehicleModelId = nvm.Id;

                                List<int> RWSWIds = new List<int>();

                                foreach (NonVehicleModelRangedWeapon a in RWSWSearch)
                                {
                                    if (a.NonVehicleModelId == RWSR.NonVehicleModelId && a.NonVehicleModelId == RWSR.NonVehicleModelId)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        RWSWIds.Add(a.Id);
                                    }

                                }

                                if (!RWSWIds.Contains(RWSR.Id))
                                {
                                    context.NonVehicleModelRangedWeapons.Add(RWSR);
                                    context.SaveChanges();
                                }
                            }

                        }

                        List<int> AddedRWeaponIDs = new List<int>();

                        foreach (RangedWeapon a in AddedRange)
                        {
                            AddedRWeaponIDs.Add(a.Id);
                        }

                        IQueryable<NonVehicleModelRangedWeapon> obsoletenvmRW =
                            (from rangedweaponspecialrule in context.NonVehicleModelRangedWeapons
                             where rangedweaponspecialrule.NonVehicleModelId == NonVehicleModel.Id && !AddedRWeaponIDs.Contains(rangedweaponspecialrule.RangedWeaponId)
                             select rangedweaponspecialrule);
                        if (obsoletenvmRW != null)
                        {
                            foreach (NonVehicleModelRangedWeapon a in obsoletenvmRW)
                            {
                                context.NonVehicleModelRangedWeapons.Remove(a);
                            }

                            context.SaveChanges();
                        }

                        //Check for existing Melee Weapons and associated M to M relationship entities, then remove or create as needed.
                        foreach (MeleeWeapon s in AddedMelee)
                        {
                            nvm = context.NonVehicleModels.Find(nvm.Id);
                            if (nvm != null)
                            {
                                IQueryable<NonVehicleModelMeleeWeapon> RWSWSearch =
                                    (from n in context.NonVehicleModelMeleeWeapons
                                     where n.NonVehicleModelId == nvm.Id
                                     select n);


                                var RWSR = new NonVehicleModelMeleeWeapon();
                                RWSR.Id = context.NonVehicleModelMeleeWeapons.Count() + 1;
                                RWSR.NonVehicleModelId = nvm.Id;

                                List<int> RWSWIds = new List<int>();

                                foreach (NonVehicleModelMeleeWeapon a in RWSWSearch)
                                {
                                    if (a.NonVehicleModelId == RWSR.NonVehicleModelId && a.NonVehicleModelId == RWSR.NonVehicleModelId)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        RWSWIds.Add(a.Id);
                                    }

                                }

                                if (!RWSWIds.Contains(RWSR.Id))
                                {
                                    context.NonVehicleModelMeleeWeapons.Add(RWSR);
                                    context.SaveChanges();
                                }
                            }

                        }

                        List<int> AddedMWeaponIDs = new List<int>();

                        foreach (RangedWeapon a in AddedRange)
                        {
                            AddedRWeaponIDs.Add(a.Id);
                        }

                        IQueryable<NonVehicleModelMeleeWeapon> obsoletenvmMW =
                            (from rangedweaponspecialrule in context.NonVehicleModelMeleeWeapons
                             where rangedweaponspecialrule.NonVehicleModelId == NonVehicleModel.Id && !AddedMWeaponIDs.Contains(rangedweaponspecialrule.MeleeWeaponIdtoModel)
                             select rangedweaponspecialrule);
                        if (obsoletenvmRW != null)
                        {
                            foreach (NonVehicleModelMeleeWeapon a in obsoletenvmMW)
                            {
                                context.NonVehicleModelMeleeWeapons.Remove(a);
                            }

                            context.SaveChanges();
                        }

                    }                    
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }
                else if (VehicleCheck.IsChecked == true)
                {
                    VehicleModel vm = new VehicleModel();
                    
                    vm.Name = ModelName.Text;
                    vm.M = int.Parse(VMValue.Text);
                    vm.Bs = int.Parse(VBSValue.Text);
                    vm.FrontAv = int.Parse(FrontAVValue.Text);
                    vm.SideAv = int.Parse(SideAVValue.Text);
                    vm.RearAv = int.Parse(RearAVValue.Text);
                    vm.Hp = int.Parse(HPValue.Text);
                    vm.Capacity = int.Parse(CapacityValue.Text);
                    vm.Type = VTypesValue.Text;
                    vm.Traits = VTraitsValue.Text;

                    try
                    {
                        var nvmAdd = vm;
                        context.VehicleModels.Update(nvmAdd);
                        context.SaveChanges();
                        ConfirmationLabel.Content = nvmAdd.Name + " updated Successfully!";

                        foreach (SpecialRule s in AddedRules)
                        {
                            vm = context.VehicleModels.Find(vm.Id);
                            if (vm != null)
                            {
                                IQueryable<VehicleModelSpecialRule> RWSWSearch =
                                    (from n in context.VehicleModelSpecialRules
                                     where n.VehicleModelId == vm.Id
                                     select n);


                                var RWSR = new VehicleModelSpecialRule();
                                RWSR.Id = context.VehicleModelSpecialRules.Count() + 1;
                                RWSR.VehicleModelId = vm.Id;

                                List<int> RWSWIds = new List<int>();

                                foreach (VehicleModelSpecialRule a in RWSWSearch)
                                {
                                    if (a.VehicleModelId == RWSR.VehicleModelId && a.VehicleModelId == RWSR.VehicleModelId)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        RWSWIds.Add(a.Id);
                                    }

                                }

                                if (!RWSWIds.Contains(RWSR.Id))
                                {
                                    context.VehicleModelSpecialRules.Add(RWSR);
                                    context.SaveChanges();
                                }
                            }

                        }

                        List<int> AddedRuleIDs = new List<int>();

                        foreach (SpecialRule a in AddedRules)
                        {
                            AddedRuleIDs.Add(a.Id);
                        }

                        IQueryable<VehicleModelSpecialRule> obsoleteRWSW =
                            (from rangedweaponspecialrule in context.VehicleModelSpecialRules
                             where rangedweaponspecialrule.VehicleModelId == nvmAdd.Id && !AddedRuleIDs.Contains(rangedweaponspecialrule.SpecialRulesId)
                             select rangedweaponspecialrule);
                        if (obsoleteRWSW != null)
                        {
                            foreach (VehicleModelSpecialRule a in obsoleteRWSW)
                            {
                                context.VehicleModelSpecialRules.Remove(a);
                            }

                            context.SaveChanges();
                        }

                        //Check for existing Ranged Weapons and associated M to M relationship entities, then remove or create as needed.

                        foreach (RangedWeapon s in AddedRange)
                        {
                            vm = context.VehicleModels.Find(vm.Id);
                            if (vm != null)
                            {
                                IQueryable<VehicleModelRangedWeapon> RWSWSearch =
                                    (from n in context.VehicleModelRangedWeapons
                                     where n.VehicleModelId == vm.Id
                                     select n);


                                var RWSR = new VehicleModelRangedWeapon();
                                RWSR.Id = context.VehicleModelRangedWeapons.Count() + 1;
                                RWSR.VehicleModelId = vm.Id;

                                List<int> RWSWIds = new List<int>();

                                foreach (VehicleModelRangedWeapon a in RWSWSearch)
                                {
                                    if (a.VehicleModelId == RWSR.VehicleModelId && a.VehicleModelId == RWSR.VehicleModelId)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        RWSWIds.Add(a.Id);
                                    }

                                }

                                if (!RWSWIds.Contains(RWSR.Id))
                                {
                                    context.VehicleModelRangedWeapons.Add(RWSR);
                                    context.SaveChanges();
                                }
                            }

                        }

                        List<int> AddedRWeaponIDs = new List<int>();

                        foreach (RangedWeapon a in AddedRange)
                        {
                            AddedRWeaponIDs.Add(a.Id);
                        }

                        IQueryable<VehicleModelRangedWeapon> obsoletenvmRW =
                            (from rangedweaponspecialrule in context.VehicleModelRangedWeapons
                             where rangedweaponspecialrule.VehicleModelId == nvmAdd.Id && !AddedRWeaponIDs.Contains(rangedweaponspecialrule.RangedWeaponId)
                             select rangedweaponspecialrule);
                        if (obsoletenvmRW != null)
                        {
                            foreach (VehicleModelRangedWeapon a in obsoletenvmRW)
                            {
                                context.VehicleModelRangedWeapons.Remove(a);
                            }

                            context.SaveChanges();
                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }
                }
            }
        }

        private void DeleteModelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    //Run deletion for nonvehicle model, including all associated relation entities with it's ID.
                    if (NonVehicleCheck.IsChecked == true)
                    {
                        try
                        {
                            IQueryable<NonVehicleModelSpecialRule> nvmsr =
                                from rwsrpick in context.NonVehicleModelSpecialRules
                                where rwsrpick.NonVehicleModelId == nvmSelection.Id
                                select rwsrpick;

                            foreach (NonVehicleModelSpecialRule a in nvmsr)
                            {
                                context.NonVehicleModelSpecialRules.Remove(a);
                            }

                            IQueryable<NonVehicleModelRangedWeapon> nvmrw =
                                from rwsrpick in context.NonVehicleModelRangedWeapons
                                where rwsrpick.NonVehicleModelId == nvmSelection.Id
                                select rwsrpick;

                            foreach (NonVehicleModelRangedWeapon a in nvmrw)
                            {
                                context.NonVehicleModelRangedWeapons.Remove(a);
                            }

                            IQueryable<NonVehicleModelMeleeWeapon> nvmmw =
                                from rwsrpick in context.NonVehicleModelMeleeWeapons
                                where rwsrpick.NonVehicleModelId == nvmSelection.Id
                                select rwsrpick;

                            foreach (NonVehicleModelMeleeWeapon a in nvmmw)
                            {
                                context.NonVehicleModelMeleeWeapons.Remove(a);
                            }

                            context.NonVehicleModels.Remove(nvmSelection);
                            context.SaveChanges();

                            AddedRules.Clear();
                            AddedMelee.Clear();
                            AddedRange.Clear();
                            setWeaponContext();
                            setNVModelContext();

                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }
                    }
                    ////Run deletion for vehicle model, including all associated relation entities with it's ID.
                    else if (VehicleCheck.IsChecked == true)
                    {
                        try
                        {
                            IQueryable<VehicleModelSpecialRule> vmsr =
                                from rwsrpick in context.VehicleModelSpecialRules
                                where rwsrpick.VehicleModelId == vmSelection.Id
                                select rwsrpick;

                            foreach (VehicleModelSpecialRule a in vmsr)
                            {
                                context.VehicleModelSpecialRules.Remove(a);
                            }

                            IQueryable<VehicleModelRangedWeapon> vmrw =
                                from rwsrpick in context.VehicleModelRangedWeapons
                                where rwsrpick.VehicleModelId == vmSelection.Id
                                select rwsrpick;

                            foreach (VehicleModelRangedWeapon a in vmrw)
                            {
                                context.VehicleModelRangedWeapons.Remove(a);
                            }

                            context.VehicleModels.Remove(vmSelection);
                            context.SaveChanges();

                            AddedRules.Clear();
                            AddedMelee.Clear();
                            AddedRange.Clear();
                            setWeaponContext();
                            setVModelContext();

                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }
                    }
                    else
                    {
                        ConfirmationLabel.Content = "Error with deletion.";
                    }


                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
