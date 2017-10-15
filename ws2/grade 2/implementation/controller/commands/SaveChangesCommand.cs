using System;
using System.Collections.Generic;

namespace MemberRegistry.controller 
{
    class SaveChangesCommand : BaseCommand
    {
        public SaveChangesCommand(string description, view.IView view, model.MemberLedger ledger) 
        : base(description, view, ledger)
        {}

        public override void ExecuteCommand() {
            DisplayMessage("Changes will be saved.");
            _ledger.SaveMemberList();
        }
    }
}