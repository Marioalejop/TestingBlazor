pipeline {
    agent any

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
                bat "mkdir %TEST_RESULTS%"
                bat "dotnet test --no-build --configuration %CONFIGURATION% --logger \"junit;LogFileName=tests.xml\" --results-directory %TEST_RESULTS%"
            }
            post {
                always {
                    echo 'Publicando resultados de pruebas...'
                    junit allowEmptyResults: false, testResults: '**/TestResults/*.xml'
                }
            }
        }

        stage('Publish Artifacts') {
            when {
                anyOf { branch 'main'; branch 'develop' }
            }
            steps {
                echo "Publicando artefactos de ${env.BRANCH_NAME}..."
                bat "dotnet publish --configuration %CONFIGURATION% -o output"
                archiveArtifacts artifacts: 'output/**/*.*', fingerprint: true
            }
        }
    }

    post {
        success {
            echo '✅ Pipeline completado exitosamente.'
        }
        failure {
            echo '❌ Pipeline fallido.'
        }
        always {
            echo '🏁 Pipeline finalizado.'
        }
    }
}
