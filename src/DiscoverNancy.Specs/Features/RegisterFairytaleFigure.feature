Feature: Register new fairy tale figure
	In order to not go crazy with stories about the same fairytale figures
	As a reader of children stories
	I want to register new fairytale figures

Scenario: Register fairytale figure without hangarounds
	Given there's no figures registered
	When I register a fairytale figure named: 'RainBlack' that is 'Evil'
	Then a new fairytale figure named: 'RainBlack' should have been registred
		And the fairytale figure should be 'Evil'
		And the fairytale figure should have '0' hangarounds 

Scenario: Register fariytale figures with hangarounds
	Given there's no figures registered
	When I register an 'Evil' fariytale figure with '8' hangarounds
	Then the fairytale figure should have '8' hangarounds 
		And every hangaround should be 'Evil'
