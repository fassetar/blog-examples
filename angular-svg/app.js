angular.element(document).ready(function() {
  var app = angular.module('angularSVG', []);
  
	app.controller('MainCtrl', function($scope) {
		$scope.graph = {'width': 300, 'height': 100};

		$scope.points = [
			{'x': 3, 'y': 7},
			{'x': 5, 'y': 15},
			{'x': 7, 'y': 8},
			{'x': 11, 'y': 17},
			{'x': 13, 'y': 13},
			{'x': 17, 'y': 23}
		];

		x = d3.time.scale().range([0, $scope.graph.width]);
		y = d3.scale.linear().range([$scope.graph.height, 0]);

		x.domain(d3.extent($scope.points, function(d) {return d.x}));
		y.domain(d3.extent($scope.points, function(d) {return d.y}));

		$scope.line = d3.svg.line()
			.x(function(d) {return x(d.x);})
			.y(function(d) {return y(d.y);});

	});

	angular.bootstrap(document, ['angularSVG']);
});