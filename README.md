WCF.Validation
==============

Handle WCF validation similar to MVC with ModelState

- Run WCF Validation Demo Web FIRST
- Run WCF Validation Demo Client Second

Relevant parts

###Web.Config

`
  <extensions>
      <behaviorExtensions>
        <add name="Validator" type="WCF.Validation.ValidationElement, WCF.Validation" />
      </behaviorExtensions>
    </extensions>
    <behaviors>
      <serviceBehaviors>
        <behavior name="">
          <serviceMetadata httpGetEnabled="true" httpsGetEnabled="true" />
          <serviceDebug includeExceptionDetailInFaults="false" />
          <Validator />
        </behavior>
      </serviceBehaviors>
    </behaviors>
`

###Contracts

`
    [DataContract]
    public class TestRequest : RequestBase
    {
        [Required]
        [DataMember]
        public string Name { get; set; }
    }
`
