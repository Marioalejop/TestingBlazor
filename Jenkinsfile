pipeline {
    agent any

    tools {
        // Asegura que Jenkins tenga .NET SDK instalado con este nombre en Global Tool Configuration
        dotnet 'dotnet6'
    }

    environment {
        CONFIGURATION = 'Release'
        TEST_RESULTS = 'TestResults'
    }

    stages {

        stage('Restore') {
            steps {
                echo 'Restaurando dependencias...'
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo 'Compilando proyecto...'
                bat "dotnet build --configuration %CONFIGURATION% --no-restore"
            }
        }

        stage('Test') {
            steps {
                echo 'Ejecutando pruebas unitarias...'
                // Crea carpeta de resultados
                bat "mkdir %TEST_RESULTS%"
                
                // Ejecuta pruebas y genera reporte TRX y cobertura
                bat "dotnet test --no-build --configuration %CONFIGURATION% --logger \"trx;LogFileName=tests.trx\" --results-directory %TEST_RESULTS%"
            }
            post {
                always {
                    echo 'Publicando resultados de pruebas...'
                    // Publica resultados en Jenkins si tienes el plugin JUnit o MSTest
                    junit allowEmptyResults: true, testResults: '**/TestResults/*.trx'
                }
                failure {
                    echo 'Las pruebas fallaron.'
                }
                success {
                    echo 'Todas las pruebas pasaron correctamente.'
                }
            }
        }

        stage('Publish') {
            when {
                branch 'main'
            }
            steps {
                echo 'Publicando artefactos'
                bat "dotnet publish --configuration %CONFIGURATION% -o output"
                archiveArtifacts artifacts: 'output/**/*.*', fingerprint: true
            }
        }
    }

    post {
        always {
            echo 'Pipeline finalizado.'
        }
        failure {
            echo 'El pipeline fallo. Revisa los logs para mas detalles.'
        }
    }
}
