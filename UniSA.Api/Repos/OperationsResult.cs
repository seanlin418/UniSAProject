using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniSA.Api.Repos
{
    public class OpResult
    {
        public OpResult(string [] result = null)
        {
            if(result != null)
                this._Errors.AddRange(result);
        }

        public OpResult(string result)
        {
            if(result != "")
                this._Errors.Add(result);
        }

        private bool _Succeeded = true;
        public bool Succeeded {
            get {
                this._Succeeded = _InnerResults == null ? true : _InnerResults.Succeeded;
                return !this._Errors.Any() && this._Succeeded; 
            }
        }
        private List<string> _Errors = new List<string>();
        public IEnumerable<string> Errors { get; }

        public IEnumerable<string> GetAggregatedErrors()
        {
            if (_InnerResults != null)
                    _Errors.AddRange(_InnerResults.Errors);
                return _Errors;
        }

        public void Add(IdentityResult result)
        {
            if (result.Succeeded) return;
            this._Errors.AddRange(result.Errors);
        }

        public void Add(string result)
        {
            this._Errors.Add(result);
        }

        private OpResult _InnerResults = null;
        public OpResult InnerResults { get; set; }
    }
}