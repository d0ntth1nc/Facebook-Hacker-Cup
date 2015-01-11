var fs = require( 'fs' );

fs.readFile(process.argv[ 2 ], function( err, data ) {
  var string = data.toString();
  var lines = string.split( '\n' );
  var linesCount = parseInt( lines[ 0 ] );
  var output = '';
  for (var lineNumber = 1; lineNumber <= linesCount; lineNumber++) {
    var minNumber = Number( lines[ lineNumber ] );
    var maxNumber = minNumber;
    var charBuff = lines[ lineNumber ].split( '' );
    
    for (var j = 0, len = charBuff.length; j < len; j++) {
      for (var k = j + 1; k < len; k++) {
	var swap = charBuff[j];
	charBuff[j] = charBuff[k];
	charBuff[k] = swap;
	
	if (charBuff[0] != '0'){
	  var numba = Number( charBuff.join( '' ) );
	  if (numba < minNumber) {
	   minNumber = numba; 
	  } else if (numba > maxNumber) {
	   maxNumber = numba; 
	  }
	}
	
	var swap = charBuff[j];
	charBuff[j] = charBuff[k];
	charBuff[k] = swap;
      }
    }
    
    output += "Case #" + lineNumber + ": " + minNumber + " " + maxNumber + '\n';
  }
  
  fs.writeFile("output.txt", output);
});