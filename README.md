WCF.Validation
==============

Handle WCF validation similar to MVC with ModelState

- Run WCF Validation Demo Web FIRST
- Run WCF Validation Demo Client Second

Relevant parts

###Web.Config

<pre>
  &lt;extensions&gt;
      &lt;behaviorExtensions&gt;
        &lt;add name=&quot;Validator&quot; type=&quot;WCF.Validation.ValidationElement, WCF.Validation&quot; /&gt;
      &lt;/behaviorExtensions&gt;
    &lt;/extensions&gt;
    &lt;behaviors&gt;
      &lt;serviceBehaviors&gt;
        &lt;behavior name=&quot;&quot;&gt;
          &lt;serviceMetadata httpGetEnabled=&quot;true&quot; httpsGetEnabled=&quot;true&quot; /&gt;
          &lt;serviceDebug includeExceptionDetailInFaults=&quot;false&quot; /&gt;
          &lt;Validator /&gt;
        &lt;/behavior&gt;
      &lt;/serviceBehaviors&gt;
    &lt;/behaviors&gt;&lt;extensions&gt;
      &lt;behaviorExtensions&gt;
        &lt;add name=&quot;Validator&quot; type=&quot;WCF.Validation.ValidationElement, WCF.Validation&quot; /&gt;
      &lt;/behaviorExtensions&gt;
    &lt;/extensions&gt;
    &lt;behaviors&gt;
      &lt;serviceBehaviors&gt;
        &lt;behavior name=&quot;&quot;&gt;
          &lt;serviceMetadata httpGetEnabled=&quot;true&quot; httpsGetEnabled=&quot;true&quot; /&gt;
          &lt;serviceDebug includeExceptionDetailInFaults=&quot;false&quot; /&gt;
          &lt;!-- &lt;Validator requestValidator=&quot;WCF.Validation.Demo.Web.SomeOtherValidator, WCF.Validation.Demo.Web&quot; /&gt; --&gt;
          &lt;Validator /&gt;
        &lt;/behavior&gt;
      &lt;/serviceBehaviors&gt;
    &lt;/behaviors&gt;
</pre>
###Contracts


    [DataContract]
    public class TestRequest : RequestBase
    {
        [Required]
        [DataMember]
        public string Name { get; set; }
    }
    
    [DataContract]
    public class ResponseBase : IHaveValidationErrors
    {
        protected ResponseBase(RequestBase requestBase) : this()
        {
            RequestId = requestBase.MessageId;
        }

        [DataMember]
        public Guid RequestId { get; set; }

        public ResponseBase()
        {
            Errors = new List<ValidationError>();
        }

        [DataMember]
        public List<ValidationError> Errors { get; set; }

        public void AddValidationError(string memberName, string errorMessage)
        {
            Errors.Add(new ValidationError
                       {
                           MemberName = memberName,
                           Message = errorMessage,
                       });
        }
    }

