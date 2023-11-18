import numpy as np
import tensorflow as tf
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import MinMaxScaler


def main():

    Sequential = tf.keras.models.Sequential
    Dense = tf.keras.layers.Dense


    #creating fake data for now to test
    np.random.seed(42)
    num_samples, num_features = 1000, 5 #The 5 features: alpha, beta, gamma, theta, delta
    eeg_data = np.random.rand(num_samples, num_features)
    fear_scores = np.random.rand(num_samples, 1)

    X_train, X_test, y_train, y_test = train_test_split(eeg_data, fear_scores, test_size=0.2, random_state=42)

    #normalize features
    scaler = MinMaxScaler()
    X_train_scaled = scaler.fit_transform(X_train)
    X_test_scaled = scaler.transform(X_test)

    model = Sequential([
        Dense(1, input_dim=num_features, activation='sigmoid') #using sigmoid instead of linear because not sure why linear is sometimes giving negative predictions
    ])
    model.compile(optimizer='adam', loss='mean_squared_error', metrics=['mae'])

    model.fit(X_train_scaled, y_train, epochs=1, batch_size=32, validation_data=(X_test_scaled, y_test)) #1 epoch is def not enough but using 1 for testing since data is fake anyways

    loss, mae = model.evaluate(X_test_scaled, y_test)
    # print(f'Mean Absolute Error on Test Set: {mae}')

    #test_data will be swapped for what we want to test
    test_data = np.random.rand(5, num_features)
    new_data_scaled = scaler.transform(test_data)
    predictions = model.predict(new_data_scaled)


    for i, pred in enumerate(predictions):
        print(f'Sample {i + 1}: Predicted Fear Score: {round(pred[0]*10)}') #multiply by 10 since output is in the range[0, 1]
    

    # model.save('test_model.keras')


if __name__ == "__main__":
    main()