;
(function ($, undefined) {

    var data = [
        {
            value: 'John Smith',
            tokens: ['John', 'Smith'],
            name: 'John Smith',
            dob: '01/01/1970',
            postcode: 'AB1 2CD'
        },
        {
            value: 'Julie Smith',
            tokens: ['Julie', 'Smith'],
            name: 'Julie Smith',
            dob: '01/01/1980',
            postcode: 'AB1 3CD'
        },
         {
             value: 'Reggie Smith',
             tokens: ['Reggie', 'Smith'],
             name: 'Reggie Smith',
             dob: '01/01/1990',
             postcode: 'AB1 4CD'
         }
    ];

    $('#mainSearch').typeahead({
        name: 'twitter-oss',
        local: data,
        template: [
            '<p class="search-header">{{name}}</p>',
            '<div class="row"><div class="col-md-6 search-label"><small>Date of birth</small></div><div class="col-md-6"><small>{{dob}}</small></div></div>',
            '<div class="row"><div class="col-md-6 search-label"><small>Postcode</small></div><div class="col-md-6"><small>{{postcode}}</small></div></div>',
            '<hr />'
        ].join(''),
        engine: Hogan
    });

    $('.typeahead.input-sm').siblings('input.tt-hint').addClass('hint-small');
    $('.typeahead.input-lg').siblings('input.tt-hint').addClass('hint-large');

})(window.jQuery);