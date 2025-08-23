using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WS318_Project_Bowen
{
    /// <summary>
    /// Interaction logic for EditWeaponWindow.xaml
    /// </summary>
    public partial class EditWeaponWindow : Window
    {

        SpecialRule selection = new SpecialRule();

        MeleeWeapon selectedMelee = new MeleeWeapon();

        RangedWeapon selectedRanged = new RangedWeapon();

        ObservableCollection<SpecialRule> AddedRules = new ObservableCollection<SpecialRule> { };

        public EditWeaponWindow()
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

        //method called upon swapping to melee weapons to display appropriate weapons.
        public void setMeleeWeaponContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var allMeleeWeapons = context.MeleeWeapons.ToList();
                WeaponGrid.ItemsSource = allMeleeWeapons;
            }
        }

        //method called upon swapping to ranged weapons to display appropriate weapons.
        public void setRangedWeaponContext()
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                var allRangedWeapons = context.RangedWeapons.ToList();
                WeaponGrid.ItemsSource = allRangedWeapons;
            }
        }

        private void WeaponGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (MeleeWeaponCheck.IsChecked == true && WeaponGrid.SelectedItem != null)
            {
                selectedMelee = (MeleeWeapon)WeaponGrid.SelectedItem;

                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    IEnumerable<MeleeWeaponSpecialRule> reference =
                        from meleeweapon in context.MeleeWeaponSpecialRules
                        where meleeweapon.MeleeWeaponId.Equals(selectedMelee.Id)
                        select meleeweapon;

                    IEnumerable<SpecialRule> rules;

                    foreach (var weapon in reference.ToList())
                    {
                        rules =
                            from specialrule in context.SpecialRules
                            where specialrule.Id.Equals(weapon.SpecialRuleId)
                            select specialrule;

                        AddedRulesGrid.ItemsSource = rules.ToList();
                    }
                    
                }
            }
            else if (RangedWeaponCheck.IsChecked == true && WeaponGrid.SelectedItem != null) 
            {
                try
                {
                    selectedRanged = (RangedWeapon)WeaponGrid.SelectedItem;

                    using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                    {
                        IEnumerable<RangedWeaponSpecialRule> reference =
                            from rangedweapon in context.RangedWeaponSpecialRules
                            where rangedweapon.RangedWeaponId.Equals(selectedRanged.Id)
                            select rangedweapon;

                        IEnumerable<SpecialRule> rules;

                        foreach (var weapon in reference.ToList())
                        {
                            rules =
                                from specialrule in context.SpecialRules
                                where specialrule.Id.Equals(weapon.SpecialRuleId)
                                select specialrule;

                            AddedRulesGrid.ItemsSource = rules.ToList();
                        }

                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            } else
            {
                ConfirmationLabel.Content = "ERROR, PLEASE SELECT A WEAPON TYPE AND WEAPON.";
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
            } catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void PickWeaponRule_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
            {
                try
                {
                    if (RangedWeaponCheck.IsChecked == true)
                    {
                        RangedWeapon rw = (RangedWeapon)WeaponGrid.SelectedItem;

                        WeaponName.Text = rw.Name;
                        RangeValue.Text = rw.Range.ToString();
                        FPValue.Text = rw.Fp.ToString();
                        RSValue.Text = rw.Rs.ToString();
                        RangedAPValue.Text = rw.Ap.ToString();
                        RangedDValue.Text = rw.D.ToString();
                        RangedTraitsValue.Text = rw.Traits;

                        IQueryable<RangedWeaponSpecialRule> rulesLinq =
                            from rwsr in context.RangedWeaponSpecialRules
                            where rwsr.RangedWeaponId == rw.Id
                            select rwsr;

                        List<int> ruleIDs = new List<int>();

                        foreach (RangedWeaponSpecialRule a in rulesLinq.ToList())
                        {
                            ruleIDs.Add(a.SpecialRuleId);
                        }

                        IQueryable<SpecialRule> rulescontainer =
                            from sr in context.SpecialRules
                            where ruleIDs.Contains(sr.Id)
                            select sr;

                        AddedRules.Clear();

                        foreach (SpecialRule a in rulescontainer.ToList())
                        {
                            AddedRules.Add(a);
                        }

                        AddedRulesGrid.ItemsSource = AddedRules;

                    }
                    else if (MeleeWeaponCheck.IsChecked == true)
                    {
                        MeleeWeapon mw = (MeleeWeapon)WeaponGrid.SelectedItem;

                        WeaponName.Text = mw.Name;
                        IMValue.Text = mw.Im.ToString();
                        AMValue.Text = mw.Am.ToString();
                        ASValue.Text = mw.As.ToString();
                        MeleeAPValue.Text = mw.Ap.ToString();
                        MeleeDValue.Text = mw.D.ToString();
                        MeleeTraitsValue.Text = mw.Traits;

                        IQueryable<MeleeWeaponSpecialRule> rulesLinq =
                            from rwsr in context.MeleeWeaponSpecialRules
                            where rwsr.MeleeWeaponId == mw.Id
                            select rwsr;

                        List<int> ruleIDs = new List<int>();

                        foreach (MeleeWeaponSpecialRule a in rulesLinq.ToList())
                        {
                            ruleIDs.Add(a.SpecialRuleId);
                        }

                        IQueryable<SpecialRule> rulescontainer =
                            from sr in context.SpecialRules
                            where ruleIDs.Contains(sr.Id)
                            select sr;

                        AddedRules.Clear();

                        foreach (SpecialRule a in rulescontainer.ToList())
                        {
                            AddedRules.Add(a);
                        }

                        AddedRulesGrid.ItemsSource = AddedRules;
                    }
                } catch (Exception ex)                
                {
                    Debug.WriteLine(ex.Message);
                    ConfirmationLabel.Content = "Please select a weapon type.";
                }
            }
        }

        private void WeaponSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (RangedWeaponCheck.IsChecked == true)
            {
                string search = WeaponSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.RangedWeapons.Where(s => s.Name.StartsWith(search));
                    //if (SpecialRuleGrid.IsLoaded){ SpecialRuleGrid.ItemsSource = filteredEntities.ToList(); }
                    WeaponGrid.ItemsSource = filteredEntities.ToList();
                }
                if (WeaponSearchBar.Text.Equals("") || WeaponSearchBar.Text.IsNullOrEmpty())
                {
                    setRangedWeaponContext();
                }
            } else if (MeleeWeaponCheck.IsChecked == true)
            {
                string search = WeaponSearchBar.Text;
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    var filteredEntities = context.MeleeWeapons.Where(s => s.Name.StartsWith(search));
                    //if (SpecialRuleGrid.IsLoaded){ SpecialRuleGrid.ItemsSource = filteredEntities.ToList(); }
                    WeaponGrid.ItemsSource = filteredEntities.ToList();
                }
                if (WeaponSearchBar.Text.Equals("") || WeaponSearchBar.Text.IsNullOrEmpty())
                {
                    setMeleeWeaponContext();
                }
            }
                
            
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
                    //if (SpecialRuleGrid.IsLoaded){ SpecialRuleGrid.ItemsSource = filteredEntities.ToList(); }
                    SpecialRuleGrid.ItemsSource = filteredEntities.ToList();
                }
            }
        }

        private void AddSpecialRule_Click(object sender, RoutedEventArgs e)
        {
            try {
                AddedRules.Add(selection);
                AddedRulesGrid.ItemsSource = AddedRules;
                } catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void SaveWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (RangedWeaponCheck.IsChecked == true)
                {
                    //gather properties from input in ranged weapon boxes.
                    RangedWeapon RW = new RangedWeapon();
                    RW.Name = WeaponName.Text;
                    RW.Range = int.Parse(RangeValue.Text);
                    RW.Fp = int.Parse(FPValue.Text);
                    RW.Rs = int.Parse(RSValue.Text);
                    RW.Ap = int.Parse(RangedAPValue.Text);
                    RW.D = int.Parse(RangedDValue.Text);
                    RW.Traits = RangedTraitsValue.Text;


                    try
                    {
                        using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                        {

                            
                            //Update selected weapon to have new characteristics, then update special rules.
                            RW.Id = selectedRanged.Id;
                            var RangedWeapon = RW;
                            
                            context.RangedWeapons.Update(RangedWeapon);
                            context.SaveChanges();
                            ConfirmationLabel.Content = RangedWeapon.Name + " added Successfully!";



                            foreach (SpecialRule s in AddedRules)
                            {
                                RangedWeapon = context.RangedWeapons.Find(RangedWeapon.Id);
                                if (RangedWeapon != null)
                                {
                                    IQueryable<RangedWeaponSpecialRule> RWSWSearch =
                                        (from rangedweaponspecialrule in context.RangedWeaponSpecialRules
                                         where rangedweaponspecialrule.RangedWeaponId == RangedWeapon.Id
                                         select rangedweaponspecialrule);

                                    
                                    var RWSR = new RangedWeaponSpecialRule();

                                    try {
                                        int highestRWID = context.RangedWeaponSpecialRules.Max(r => r.Id);
                                        RWSR.Id = highestRWID + 1;
                                    } catch { RWSR.Id = context.RangedWeaponSpecialRules.Count() + 1; }
                                    
                                    RWSR.RangedWeaponId = RangedWeapon.Id;
                                    RWSR.SpecialRuleId = s.Id;

                                    List<int> RWSWIds = new List<int>();

                                    foreach (RangedWeaponSpecialRule a in RWSWSearch)
                                    {
                                        if (a.RangedWeaponId == RWSR.RangedWeaponId && a.SpecialRuleId == RWSR.SpecialRuleId)
                                        {
                                            break;
                                        } else
                                        {
                                            RWSWIds.Add(a.Id);
                                        }
                                            
                                    }

                                    if (!RWSWIds.Contains(RWSR.Id)) {
                                        context.RangedWeaponSpecialRules.Add(RWSR);
                                        context.SaveChanges(); }
                                }
                                
                            }

                            List<int> AddedRuleIDs = new List<int>();

                            foreach (SpecialRule a in AddedRules)
                            {
                                AddedRuleIDs.Add(a.Id);
                            }

                            IQueryable<RangedWeaponSpecialRule> obsoleteRWSW =
                                (from rangedweaponspecialrule in context.RangedWeaponSpecialRules
                                 where rangedweaponspecialrule.RangedWeaponId == RangedWeapon.Id && !AddedRuleIDs.Contains(rangedweaponspecialrule.SpecialRuleId)
                                 select rangedweaponspecialrule);
                            if (obsoleteRWSW != null)
                            {
                                foreach (RangedWeaponSpecialRule a in obsoleteRWSW)
                                { 
                                    context.RangedWeaponSpecialRules.Remove(a); }
                                
                                context.SaveChanges();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                else if (MeleeWeaponCheck.IsChecked == true)
                {
                    //gather properties from input in melee weapon boxes
                    MeleeWeapon MW = new MeleeWeapon();
                    MW.Name = WeaponName.Text;
                    MW.Im = (IMValue.Text);
                    MW.Am = (AMValue.Text);
                    MW.As = ASValue.Text;
                    MW.Ap = int.Parse(MeleeAPValue.Text);
                    MW.D = int.Parse(MeleeDValue.Text);
                    MW.Traits = MeleeTraitsValue.Text;


                    try
                    {
                        using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                        {
                            //increment ID to place in table by 1, try and add weapon to table, cycle through special rules and create m-m resolution entries for each that will enable them to be pulled later.
                            MW.Id = selectedMelee.Id;
                            var MeleeWeapon = MW;

                            context.MeleeWeapons.Update(MeleeWeapon);
                            context.SaveChanges();
                            ConfirmationLabel.Content = MeleeWeapon.Name + " added Successfully!";

                            foreach (SpecialRule s in AddedRules)
                            {
                                MeleeWeapon = context.MeleeWeapons.Find(MeleeWeapon.Id);
                                if (MeleeWeapon != null)
                                {
                                    IQueryable<MeleeWeaponSpecialRule> RWSWSearch =
                                        (from meleeweaponspecialrule in context.MeleeWeaponSpecialRules
                                                                  where meleeweaponspecialrule.MeleeWeaponId == MeleeWeapon.Id
                                                                  select meleeweaponspecialrule);


                                    var RWSR = new MeleeWeaponSpecialRule();
                                    try
                                    {
                                        int highestRWID = context.MeleeWeaponSpecialRules.Max(r => r.Id);
                                        RWSR.Id = highestRWID + 1;
                                    }
                                    catch { RWSR.Id = context.MeleeWeaponSpecialRules.Count() + 1; }
                                    RWSR.MeleeWeaponId = MeleeWeapon.Id;
                                    RWSR.SpecialRuleId = s.Id;

                                    List<int> RWSWIds = new List<int>();

                                    foreach (MeleeWeaponSpecialRule a in RWSWSearch)
                                    {
                                        if (a.MeleeWeaponId == RWSR.MeleeWeaponId && a.SpecialRuleId == RWSR.SpecialRuleId)
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
                                        context.MeleeWeaponSpecialRules.Add(RWSR);
                                        context.SaveChanges();
                                    }

                                }

                            }

                            List<int> AddedRuleIDs = new List<int>();

                            foreach (SpecialRule a in AddedRules)
                            {
                                AddedRuleIDs.Add(a.Id);
                            }

                            IQueryable<MeleeWeaponSpecialRule> obsoleteRWSW =
                                                        (from meleeweaponspecialrule in context.MeleeWeaponSpecialRules
                                                         where meleeweaponspecialrule.MeleeWeaponId == MeleeWeapon.Id && !AddedRuleIDs.Contains(meleeweaponspecialrule.SpecialRuleId)
                                                         select meleeweaponspecialrule);
                            if (obsoleteRWSW != null)
                            {
                                foreach (MeleeWeaponSpecialRule a in obsoleteRWSW) 
                                { context.MeleeWeaponSpecialRules.Remove(a); }
                                
                                context.SaveChanges();
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                }
                else { ConfirmationLabel.Content = "Error, please select a weapon type."; } //tell user to select radio button options to proceed.
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private void DeleteWeaponButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new GSchoolCWs318ProjectBowenDatabase1MdfContext())
                {
                    if (RangedWeaponCheck.IsChecked == true)
                    {
                        try
                        {
                            IQueryable<RangedWeaponSpecialRule> rwsr =
                                from rwsrpick in context.RangedWeaponSpecialRules
                                where rwsrpick.RangedWeaponId == selectedRanged.Id
                                select rwsrpick;

                            foreach (RangedWeaponSpecialRule a in rwsr)
                            {
                                context.RangedWeaponSpecialRules.Remove(a);
                            }

                            context.RangedWeapons.Remove(selectedRanged);
                            context.SaveChanges();

                            AddedRules.Clear();
                            setRangedWeaponContext();

                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }
                    }
                    else if (MeleeWeaponCheck.IsChecked == true)
                    {
                        try
                        {
                            IQueryable<MeleeWeaponSpecialRule> rwsr =
                                from rwsrpick in context.MeleeWeaponSpecialRules
                                where rwsrpick.MeleeWeaponId == selectedMelee.Id
                                select rwsrpick;

                            foreach (MeleeWeaponSpecialRule a in rwsr)
                            {
                                context.MeleeWeaponSpecialRules.Remove(a);
                            }

                            context.MeleeWeapons.Remove(selectedMelee);
                            context.SaveChanges();

                            AddedRules.Clear();
                            setMeleeWeaponContext();

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
        
        //toggles visibility of which weapon entry form is visible
        private void RangedWeaponCheck_Checked(object sender, RoutedEventArgs e)
        {
            MeleeWeaponCheck.IsChecked = false;
            MeleeEntry.Visibility = Visibility.Collapsed;
            RangedEntry.Visibility = Visibility.Visible;
            setRangedWeaponContext();
        }

        private void MeleeWeaponCheck_Checked(object sender, RoutedEventArgs e)
        {
            RangedWeaponCheck.IsChecked = false;
            RangedEntry.Visibility = Visibility.Collapsed;
            MeleeEntry.Visibility = Visibility.Visible;
            setMeleeWeaponContext();
        }

        private void DeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            if(AddedRulesGrid.SelectedItem != null)
            {
                try{
                    foreach (SpecialRule a in AddedRules)
                    {
                        if (a.Equals(selection.Id))
                        {
                            selection = a;
                        }
                    }

                    AddedRules.Remove(selection);
                } catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
        }
    }
}
